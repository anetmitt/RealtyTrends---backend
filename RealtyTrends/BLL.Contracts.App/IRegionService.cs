using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IRegionService : IBaseRepository<BLL.DTO.Region>,
    IRegionRepositoryCustom<BLL.DTO.Region>
{
    
}