using DAL.Contracts.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Base;

public class EFBaseUow<TDbContext> : IBaseUow
    where TDbContext : DbContext
{
    protected TDbContext UowDbContext;
    
    public EFBaseUow(TDbContext dataContext)
    {
        UowDbContext = dataContext;
    }
    
    public virtual async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}