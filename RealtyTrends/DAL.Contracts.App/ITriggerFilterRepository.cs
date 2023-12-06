using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ITriggerFilterRepository : IBaseRepository<TriggerFilter>, ITriggerFilterRepositoryCustom<TriggerFilter>
{
}

public interface ITriggerFilterRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> GetAllTriggerFiltersAsync(Guid triggerId);
}