using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class RegionTypeRepository :
    EFBaseRepository<RegionType, ApplicationDbContext>, IRegionTypeRepository
{
    public RegionTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<RegionType?> FindByNameAsync(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}