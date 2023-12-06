using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITransactionTypeService : IBaseRepository<BLL.DTO.TransactionType>,
    ITransactionTypeRepositoryCustom<BLL.DTO.TransactionType>
{
    
}