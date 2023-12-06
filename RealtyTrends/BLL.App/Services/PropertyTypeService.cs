using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;


public class PropertyTypeService: BaseEntityService<BLL.DTO.PropertyType, PropertyType, IPropertyTypeRepository>, IPropertyTypeService
{
    protected readonly IAppUow Uow;
    
    public PropertyTypeService(IMapper<DTO.PropertyType, PropertyType> mapper, IAppUow uow) : base(uow.PropertyTypeRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<DTO.PropertyType?> FindByNameAsync(string name)
    {
        return Mapper.Map(await Uow.PropertyTypeRepository.FindByNameAsync(name));
    }
    
    public async Task<IEnumerable<DTO.PropertyType>> AllAsync()
    {
        return (await Uow.PropertyTypeRepository.AllAsync()).Select(e => Mapper.Map(e));
    }
}