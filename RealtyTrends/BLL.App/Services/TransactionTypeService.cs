using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class TransactionTypeService: BaseEntityService<BLL.DTO.TransactionType, TransactionType, ITransactionTypeRepository>, ITransactionTypeService
{
    protected readonly IAppUow Uow;
    public TransactionTypeService(IMapper<DTO.TransactionType, TransactionType> mapper, IAppUow uow) : base(uow.TransactionTypeRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.TransactionType?> FindByNameAsync(string name)
    {
        return Mapper.Map(await Uow.TransactionTypeRepository.FindByNameAsync(name));
    }
    
    public async Task<IEnumerable<DTO.TransactionType>> AllAsync()
    {
        return (await Uow.TransactionTypeRepository.AllAsync()).Select(e => Mapper.Map(e));
    }
}