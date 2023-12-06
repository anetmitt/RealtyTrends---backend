using DAL.Contracts.App;
using DAL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IPropertyTypeService : IBaseRepository<BLL.DTO.PropertyType>,
    IPropertyTypeRepositoryCustom<BLL.DTO.PropertyType>
{
    
}