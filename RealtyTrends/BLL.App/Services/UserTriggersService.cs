using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Public.DTO.v1;
using FilterType = Domain.App.FilterType;
using UserTrigger = Domain.App.UserTrigger;

namespace BLL.App.Services;

public class UserTriggerService : BaseEntityService<BLL.DTO.UserTrigger, UserTrigger, IUserTriggerRepository>, IUserTriggerService
{
    protected readonly IAppUow _uow;

    public UserTriggerService(IMapper<DTO.UserTrigger, UserTrigger> mapper, IAppUow uow) : base(uow.UserTriggerRepository, mapper)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<DTO.UserTrigger>> AllAsync(Guid userId)
    {
        return (await _uow.UserTriggerRepository.AllAsync(userId)).Select(e => Mapper.Map(e));
    }

    public async Task<DTO.UserTrigger?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await _uow.UserTriggerRepository.FindAsync(id, userId));
    }

    public async Task<UserTrigger?> RemoveAsync(Guid id, Guid userId)
    {
        var removedUserTrigger = await _uow.UserTriggerRepository.RemoveAsync(id, userId);
        await _uow.SaveChangesAsync();
        return removedUserTrigger;
    }

    public async Task UpdateTriggerAsync(Guid id, Guid userId, float ppu)
    {
        await _uow.UserTriggerRepository.UpdateTriggerAsync(id, userId, ppu);
        await _uow.SaveChangesAsync();
    }

    public async Task AddNewTriggerToUser(Guid userId, StatisticFilters triggerFilters, float beginningSquareMeterPrice)
    {
        var triggerId = await AddNewTrigger(triggerFilters.TriggerName, beginningSquareMeterPrice, 
            triggerFilters.TriggerPricePerUnit);

        if (triggerFilters.CountySelect != null)
        {
            var filterTypeId =  GetFilterType("County").Result.Id;
            var county = await AddNewFilter(null, filterTypeId,triggerFilters.CountySelect, null, null);
            await AddNewTriggerFilter(triggerId, county);  
        }

        if (triggerFilters.ParishSelect != null)
        {
            var filterTypeId =  GetFilterType("Parish").Result.Id;
            var parish = await AddNewFilter(null, filterTypeId,triggerFilters.ParishSelect, null, null);
            await AddNewTriggerFilter(triggerId, parish);
        }
        
        if (triggerFilters.CitySelect != null)
        {
            var filterTypeId =  GetFilterType("City").Result.Id;
            var city = await AddNewFilter(null, filterTypeId,triggerFilters.CitySelect, null, null);
            await AddNewTriggerFilter(triggerId, city);
        }
        
        if (triggerFilters.StreetSelect != null)
        {
            var filterTypeId =  GetFilterType("Street").Result.Id;
            var street = await AddNewFilter(null, filterTypeId,triggerFilters.StreetSelect, null, null);
            await AddNewTriggerFilter(triggerId, street);
        }
        
        // Add TransactionType filter
        if (triggerFilters.TransactionType != null)
        {
            var filterTypeId =  GetFilterType("TransactionType").Result.Id;
            var transactionType = await AddNewFilter(null, filterTypeId,null, triggerFilters.TransactionType, null);
            await AddNewTriggerFilter(triggerId, transactionType);
        }
        
        // Add PropertyType filter
        if (triggerFilters.PropertyType != null)
        {
            var filterTypeId =  GetFilterType("PropertyType").Result.Id;
            var propertyType = await AddNewFilter(null, filterTypeId,null, null, triggerFilters.PropertyType);
            await AddNewTriggerFilter(triggerId, propertyType);
        }
        
        // Add AreaMin filter
        if (triggerFilters.AreaMin != null)
        {
            var filterTypeId =  GetFilterType("AreaMin").Result.Id;
            var areaMin = await AddNewFilter(triggerFilters.AreaMin, filterTypeId,null, null, null);
            await AddNewTriggerFilter(triggerId, areaMin);
        }
        
        // Add AreaMax filter
        if (triggerFilters.AreaMax != null)
        {
            var filterTypeId =  GetFilterType("AreaMax").Result.Id;
            var areaMax = await AddNewFilter(triggerFilters.AreaMax, filterTypeId,null, null, null);
            await AddNewTriggerFilter(triggerId, areaMax);
        }
        
        // Add RoomsCountMin filter
        if (triggerFilters.RoomsCountMin != null)
        {
            var filterTypeId =  GetFilterType("RoomsCountMin").Result.Id;
            var roomsCountMin = await AddNewFilter(triggerFilters.RoomsCountMin, filterTypeId,null, null, null);
            await AddNewTriggerFilter(triggerId, roomsCountMin);
        }
        
        // Add RoomsCountMax filter
        if (triggerFilters.RoomsCountMax != null)
        {
            var filterTypeId =  GetFilterType("RoomsCountMax").Result.Id;
            var roomsCountMax = await AddNewFilter(triggerFilters.RoomsCountMax, filterTypeId,null, null, null);
            await AddNewTriggerFilter(triggerId, roomsCountMax);
        }
       
        //Add the trigger to the user
        var userTrigger = new UserTrigger
        {
            TriggerId = triggerId,
            AppUserId = userId
        };
        
        _uow.UserTriggerRepository.Add(userTrigger);
        await _uow.SaveChangesAsync();
    }

    private async Task<Guid> AddNewTrigger(string name, float beginningSquareMeterPrice, float userSquareMeterPrice)
    {
        var trigger = new Domain.App.Trigger
        {
            Name = name,
            BeginningSquareMeterPrice = beginningSquareMeterPrice,
            UserSquareMeterPrice = userSquareMeterPrice
            
        };
        
        _uow.TriggerRepository.Add(trigger);
        await _uow.SaveChangesAsync();
        
        return trigger.Id;
    }

    private async Task<FilterType> GetFilterType(string name)
    {
        var filterType = await _uow.FilterTypeRepository.GetFilterTypeByName(name);
        
        if (filterType == null)
        {
            filterType = new FilterType
            {
                Name = name
            };
            
            _uow.FilterTypeRepository.Add(filterType);
            await _uow.SaveChangesAsync();
        }
        
        return filterType;
    }
    
    private async Task<Guid> AddNewFilter(float? value, Guid filterTypeId, Guid? regionId, Guid? transactionTypeId,
        Guid? propertyTypeId)
    {
        var filter = new Domain.App.Filter
        {
            Value = value,
            FilterTypeId = filterTypeId,
            RegionId = regionId,
            TransactionTypeId = transactionTypeId,
            PropertyTypeId = propertyTypeId
        };
        
        _uow.FilterRepository.Add(filter);
        await _uow.SaveChangesAsync();
        
        return filter.Id;
    }
    
    private async Task AddNewTriggerFilter(Guid triggerId, Guid filterId)
    {
        var triggerFilter = new Domain.App.TriggerFilter
        {
            TriggerId = triggerId,
            FilterId = filterId
        };
        
        _uow.TriggerFilterRepository.Add(triggerFilter);
        await _uow.SaveChangesAsync();
    }
}

