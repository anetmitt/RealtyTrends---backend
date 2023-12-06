using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IUserTriggerRepository : IBaseRepository<UserTrigger>, IUserTriggerRepositoryCustom<UserTrigger>
{
    
}

public interface IUserTriggerRepositoryCustom<TEntity>
{
    public Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    
    public Task<TEntity?> FindAsync(Guid id, Guid userId);

    public Task<UserTrigger?> RemoveAsync(Guid id, Guid userId);
    
    public Task UpdateTriggerAsync(Guid id, Guid userId, float ppu);
}