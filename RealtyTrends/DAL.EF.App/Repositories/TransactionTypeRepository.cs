using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class TransactionTypeRepository :
    EFBaseRepository<TransactionType, ApplicationDbContext>, ITransactionTypeRepository
{
    public TransactionTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<TransactionType?> FindByNameAsync(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}