using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface ITransactionTypeRepository : IBaseRepository<TransactionType> , ITransactionTypeRepositoryCustom<TransactionType>
{
}

public interface ITransactionTypeRepositoryCustom<TEntity>
{
    public Task<TEntity?> FindByNameAsync(string name);
}