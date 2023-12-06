using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class TriggerRepository :
    EFBaseRepository<Trigger, ApplicationDbContext>, ITriggerRepository
{
    public TriggerRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    // update trigger
    

}