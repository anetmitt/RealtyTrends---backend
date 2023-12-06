using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IRegionRepository : IBaseRepository<Region>, IRegionRepositoryCustom<Region>
{
}

public interface IRegionRepositoryCustom<TEntity>
{
    public Task<TEntity?> FindByNameAsync(string name);
    public Task<IEnumerable<TEntity>> AllByParentIdAsync(Guid parentId);

    public Task<IEnumerable<TEntity>> AllCountiesAsync();
    
    
}