using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PropertyRepository :
    EFBaseRepository<Property, ApplicationDbContext>, IPropertyRepository
{
    public PropertyRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Property>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyType)
            .Include(p => p.Website)
            .Include(p => p.PropertyUpdates)
            .ToListAsync();
    }

    public override async Task<Property?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyType)
            .Include(p => p.Website)
            .Include(p => p.PropertyUpdates)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public Property Add(Property property, Guid snapshotId)
    {
        
        var newPropertyUpdate = new PropertyUpdate
        {
            PropertyId = property.Id,
            SnapshotId = snapshotId
        };
        
        property.PropertyUpdates ??= new List<PropertyUpdate>();
        property.PropertyUpdates!.Add(newPropertyUpdate);
        return RepositoryDbSet.Add(property).Entity;
    }
    
    public async Task<Property?> FindByExternalIdAsync(int externalId)
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyStructures)
            .Include(p => p.PropertyUpdates)
            .FirstOrDefaultAsync(m => m.ExternalId == externalId);
    }
    
    public async Task<List<Guid>> FindPropertiesBasedOnTheirTypeAsync(Guid propertyTypeId, List<Guid> filteredPropertyIds)
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyType)
            .Where(p => p.PropertyTypeId == propertyTypeId && filteredPropertyIds.Contains(p.Id))
            .Select(p => p.Id)
            .ToListAsync();
    }
}