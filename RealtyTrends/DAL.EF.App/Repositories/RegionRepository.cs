using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class RegionRepository :
    EFBaseRepository<Region, ApplicationDbContext>, IRegionRepository
{
    public RegionRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<Region>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(r => r.Parent)
            .Include(r => r.RegionType)
            .ToListAsync();
    }

    public override async Task<Region?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(r => r.Parent)
            .Include(r => r.RegionType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<Region?> FindByNameAsync(string name)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(m => m.Name == name);
    }
    
    public async Task<IEnumerable<Region>> AllByParentIdAsync(Guid parentId)
    {
        var test= await RepositoryDbSet
            .Include(r => r.Parent)
            .Include(r => r.RegionType)
            .Where(r => r.ParentId == parentId)
            .ToListAsync();
        return test;
    }
    
    public async Task<IEnumerable<Region>> AllCountiesAsync()
    {
        return await RepositoryDbSet
            .Include(r => r.Parent)
            .Include(r => r.RegionType)
            .Where(r => r.RegionType!.Name == "County")
            .ToListAsync();
    }
}