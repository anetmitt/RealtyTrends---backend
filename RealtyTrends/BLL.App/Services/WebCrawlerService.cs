using BLL.Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Infrastructure.WebCrawler;

namespace BLL.App.Services;

public class WebCrawlerService : IService
{
    private readonly IAppUow _uow;
    
    public WebCrawlerService(IAppUow uow)
    {
        _uow = uow;
    }

    public async Task CrawlAndStoreData()
    {
        var page = 1;
        
        while (page < 2)
        {
            var data = await PropertyDataCrawler.CrawlData(500, page);
            if (data.Count == 0) { break; }
            await CreateOrUpdateRealEstateProperty(data);
            page++;
        }
    }

    public async Task CreateOrUpdateRealEstateProperty(List<RealEstateData> data)
    {
        var snapshotId = await GetSnapshotId();
        
        foreach (var realEstate in data)
        {
            var property = await _uow.PropertyRepository.FindByExternalIdAsync(int.Parse(realEstate.Id));

            if (property != null)
            {
                await PropertyUpdate(property, realEstate, snapshotId);
                continue;
            }
            
            // new/old property type
            var propertyTypeId = await GetPropertyType(realEstate.UnitType);
            
            // new/old website
            var website = await _uow.WebsiteRepository.FindByWebsiteNameAsync("City24");
            
            // new/old property
            var propertyId = await AddNewProperty(realEstate, propertyTypeId, website!.Id, snapshotId);
            
            // add property structure
            await AddPropertyStructure(realEstate, propertyId);
            
            // Get transaction type
            var transactionTypeId = GetTransactionType(realEstate.TransactionType);
            
            // new/old regions
            await AddRegionProperties(propertyId, transactionTypeId, realEstate);
        }
    }

    public async Task<Guid> GetSnapshotId()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var snapshot = await _uow.SnapshotRepository.GetSnapshotByDateAsync(currentDate);

        if (snapshot == null)
        {
            snapshot = _uow.SnapshotRepository.Add(new Snapshot { CreatedAt = currentDate });
            await _uow.SaveChangesAsync();
        }

        return snapshot.Id; 
    }
    
    public async Task PropertyUpdate(Property property, RealEstateData realEstateData, Guid snapshotId)
    {
        _uow.PropertyUpdateRepository.Add(new PropertyUpdate { PropertyId = property.Id, SnapshotId = snapshotId});
        
        var propertyLastPrice = _uow.PropertyStructureRepository.FindLastPrice(property.Id).Result!.Value;
        
        if (propertyLastPrice != realEstateData.Price)
        {
            var priceId = GetPropertyFields("Price");
            await NewPropertyStructure(property.Id, priceId, realEstateData.Price);
        }
        
        await _uow.SaveChangesAsync();
    }

    public Guid GetTransactionType(string name)
    {
        return _uow.TransactionTypeRepository.FindByNameAsync(name).Result!.Id;
    }

    public async Task AddRegionProperties(Guid propertyId, Guid transactionTypeId, RealEstateData realEstateData)
    {
        var countyId = await AddNewRegion(realEstateData.CountyName, GetRegionType("County"));
        var parishId = await AddNewRegion(realEstateData.ParishName, GetRegionType("Parish"), countyId);
        var cityId = await AddNewRegion(realEstateData.CityName, GetRegionType("City"), parishId);
        var streetId = await AddNewRegion(realEstateData.StreetName, GetRegionType("Street"), cityId);
        
        await NewRegionProperties(propertyId, transactionTypeId, countyId);
        await NewRegionProperties(propertyId, transactionTypeId, parishId);
        await NewRegionProperties(propertyId, transactionTypeId, cityId);
        await NewRegionProperties(propertyId, transactionTypeId, streetId);
        
    }

    public async Task NewRegionProperties(Guid propertyId, Guid transactionTypeId, Guid regionId)
    {
        var propertyRegion = new RegionProperty
        {
            PropertyId = propertyId,
            RegionId = regionId,
            TransactionTypeId = transactionTypeId
        };
        _uow.RegionPropertyRepository.Add(propertyRegion);
        await _uow.SaveChangesAsync();
    }

    public Guid GetRegionType(string name)
    {
        return _uow.RegionTypeRepository.FindByNameAsync(name).Result!.Id;
    }
    
    public async Task<Guid> AddNewRegion(string regionName, Guid regionTypeId, Guid? parentId = null)
    {
        var region = await _uow.RegionRepository.FindByNameAsync(regionName);

        if (region != null) return region.Id;
        
        region = new Region
        {
            Name = regionName,
            RegionTypeId = regionTypeId,
            ParentId = parentId
        };
        _uow.RegionRepository.Add(region);
        await _uow.SaveChangesAsync();

        return region.Id;
    }

    public async Task AddPropertyStructure(RealEstateData realEstate, Guid propertyId)
    {
        var priceId = GetPropertyFields("Price");
        var roomCountId = GetPropertyFields("Rooms");
        var areaId = GetPropertyFields("Area");
        var pricePerUnitId = GetPropertyFields("Price Per Unit");
        
        await NewPropertyStructure(propertyId, priceId, realEstate.Price);
        await NewPropertyStructure(propertyId, roomCountId, realEstate.RoomCount);
        await NewPropertyStructure(propertyId, areaId, realEstate.PropertySize);
        await NewPropertyStructure(propertyId, pricePerUnitId, realEstate.PricePerUnit);
    }

    public Guid GetPropertyFields(string name)
    {
        return _uow.PropertyFieldRepository.FindByNameAsync(name).Result!.Id;
    }

    public async Task NewPropertyStructure(Guid propertyId, Guid propertyFieldId, string value)
    {
        var propertyStructure = new PropertyStructure
        {
            PropertyId = propertyId,
            PropertyFieldId = propertyFieldId,
            Value = value
        };
        _uow.PropertyStructureRepository.Add(propertyStructure);
        await _uow.SaveChangesAsync();
    }
    
    public async Task<Guid> AddNewProperty(RealEstateData realEstate, Guid propertyTypeId, Guid websiteId, Guid snapshotId)
    {
        var property = await _uow.PropertyRepository.FindByExternalIdAsync(int.Parse(realEstate.Id));

        if (property != null) return property.Id;
        
        property = new Property
        {
            ExternalId = int.Parse(realEstate.Id),
            PropertyTypeId = propertyTypeId,
            WebsiteId = websiteId
        };
        _uow.PropertyRepository.Add(property, snapshotId);
        
        await _uow.SaveChangesAsync();

        return property.Id;
    }
    
    public async Task<Guid> GetPropertyType(string name)
    {
        PropertyType? propertyType = name switch
        {
            "CottageOrVilla" => await _uow.PropertyTypeRepository.FindByNameAsync("Cottage"),
            "HouseShare" => await _uow.PropertyTypeRepository.FindByNameAsync("House Share"),
            "NewProject" => await _uow.PropertyTypeRepository.FindByNameAsync("New Project"),
            "Commercial" => await _uow.PropertyTypeRepository.FindByNameAsync("Commercial Space"),
            _ => await _uow.PropertyTypeRepository.FindByNameAsync(name)
        };

        return propertyType!.Id;
    }
}
