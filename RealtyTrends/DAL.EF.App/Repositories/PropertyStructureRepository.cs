using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PropertyStructureRepository  :
    EFBaseRepository<PropertyStructure, ApplicationDbContext>, IPropertyStructureRepository
{
    public PropertyStructureRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<PropertyStructure>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyField)
            .Include(p => p.Property)
            .ToListAsync();
    }

    public override async Task<PropertyStructure?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyField)
            .Include(p => p.Property)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<PropertyStructure?> FindLastPrice(Guid propertyId)
    {
        return await RepositoryDbSet
            .Include(p => p.PropertyField)
            .Include(p => p.Property)
            .Where(p => p.PropertyId == propertyId)
            .OrderByDescending(p => p.AddedTime)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Property>> GetPropertiesByItsFieldsRange(int? minValue, int? maxValue, string fieldName, List<Guid> filteredProperties)
    {
        // First, retrieve the properties from the database based on fieldName and filteredProperties
        var properties = await RepositoryDbSet
            .Include(p => p.Property)
            .ThenInclude(p => p!.PropertyUpdates)
            .ThenInclude(p => p.Snapshot)
            .Include(p => p.PropertyField)
            .Where(p => p.PropertyField!.Name == fieldName && filteredProperties.Contains(p.PropertyId))
            .ToListAsync();

        // Then, parse the Value field and filter the properties in memory
        var filteredPropertiesByRange = properties
            .Where(p => minValue == null || maxValue == null || (int.TryParse(p.Value, out var parsedValue) && parsedValue >= minValue && parsedValue <= maxValue))
            .Select(p => p.Property!)
            .ToList();

        return filteredPropertiesByRange;
    }

}