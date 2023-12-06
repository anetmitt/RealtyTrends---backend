using BLL.Base;
using BLL.Contracts.App;
using BLL.DTO;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class TriggersService : BaseEntityService<BLL.DTO.Trigger, Domain.App.Trigger, ITriggerRepository>, ITriggersService
{
    protected readonly IAppUow Uow;


    public TriggersService(IMapper<BLL.DTO.Trigger, Domain.App.Trigger> mapper, IAppUow uow) : base(uow.TriggerRepository, mapper)
    {
        Uow = uow;
    }
    
    public async Task<BLL.DTO.Trigger?> FindAsync(Guid triggerId)
    {
        return Mapper.Map(await Uow.TriggerRepository.FindAsync(triggerId));
    }
   
}