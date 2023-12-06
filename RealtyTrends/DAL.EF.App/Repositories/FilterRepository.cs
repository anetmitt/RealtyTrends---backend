using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class FilterRepository :
    EFBaseRepository<Filter, ApplicationDbContext>, IFilterRepository
{
    public FilterRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Filter>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(f => f.FilterType)
            .OrderBy(f => f.Value)
            .ToListAsync();
    }

    public override async Task<Filter?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(f => f.FilterType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}