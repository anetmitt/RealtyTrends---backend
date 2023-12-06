using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface ITriggerFiltersService : IBaseRepository<BLL.DTO.TriggerFilter>,
    ITriggerFilterRepositoryCustom<BLL.DTO.TriggerFilter>
{
    
}