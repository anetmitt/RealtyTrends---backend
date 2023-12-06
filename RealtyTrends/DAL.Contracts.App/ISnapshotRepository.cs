using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ISnapshotRepository : IBaseRepository<Snapshot>, ISnapshotRepositoryCustom<Snapshot>
{
    
}

public interface ISnapshotRepositoryCustom<TEntity>
{
    public Task<TEntity?> GetSnapshotByDateAsync(DateOnly date);
}