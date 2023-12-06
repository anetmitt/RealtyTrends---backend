using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class PropertyTypeMapper: BaseMapper<BLL.DTO.PropertyType, Domain.App.PropertyType>
{
    public PropertyTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}