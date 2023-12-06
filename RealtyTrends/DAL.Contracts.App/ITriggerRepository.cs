using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ITriggerRepository : IBaseRepository<Trigger>, ITriggerRepositoryCustom<Trigger>
{
    
}

public interface ITriggerRepositoryCustom<TEntity>
{
}