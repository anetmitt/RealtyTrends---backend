using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PropertyUpdateRepository : EFBaseRepository<PropertyUpdate, ApplicationDbContext>, IPropertyUpdateRepository
{
    public PropertyUpdateRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<PropertyUpdate>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(p => p.Property)
            .Include(p => p.Snapshot)
            .ToListAsync();
    }

    public override async Task<PropertyUpdate?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(p => p.Property)
            .Include(p => p.Snapshot)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<PropertyUpdate?> FindByPropertyAsync(Guid propertyId)
    {
        return await RepositoryDbSet
            .Include(p => p.Snapshot)
            .FirstOrDefaultAsync(m => m.PropertyId == propertyId);
    }
    
    public async Task<List<Guid>> FindPropertyIdsByDateRangeAsync(DateOnly startDate, DateOnly endDate)
    {
        return await RepositoryDbSet
            .Include(pu => pu.Property)
            .Where(pu => pu.Snapshot!.CreatedAt >= startDate && pu.Snapshot.CreatedAt <= endDate)
            .Select(pu => pu.PropertyId)  // get the IDs of the Properties in the date range
            .ToListAsync();
    }
}