using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITriggersService : IBaseRepository<BLL.DTO.Trigger>,
    ITriggerRepositoryCustom<BLL.DTO.Trigger>
{
    
}