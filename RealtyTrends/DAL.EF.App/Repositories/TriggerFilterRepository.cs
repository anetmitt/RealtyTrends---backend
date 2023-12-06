using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class TriggerFilterRepository :
    EFBaseRepository<TriggerFilter, ApplicationDbContext>, ITriggerFilterRepository
{
    public TriggerFilterRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<TriggerFilter>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(t => t.Filter)
            .Include(t => t.Trigger)
            .ToListAsync();
    }
    
    public virtual async Task<IEnumerable<TriggerFilter>> GetAllTriggerFiltersAsync(Guid triggerId)
    {
        return await RepositoryDbSet
            .Include(t => t.Filter)
            .ThenInclude(t => t!.FilterType)
            .Include(t => t.Filter)
            .ThenInclude(t => t!.TransactionType)
            .Include(t => t.Filter)
            .ThenInclude(t => t!.PropertyType)
            .Where(t => t.TriggerId == triggerId)
            .Include(t=> t.Filter)
            .ThenInclude(t => t!.Region)
            .ToListAsync();
    }

    public override async Task<TriggerFilter?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(t => t.Filter)
            .Include(t => t.Trigger)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}