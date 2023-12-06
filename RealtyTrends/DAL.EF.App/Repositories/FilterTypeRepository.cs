using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class FilterTypeRepository : EFBaseRepository<FilterType, ApplicationDbContext>, IFilterTypeRepository
{
    public FilterTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<FilterType?> GetFilterTypeByName(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(ft => ft.Name == name);
    }
}