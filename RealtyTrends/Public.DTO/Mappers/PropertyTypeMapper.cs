using AutoMapper;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class PropertyTypeMapper : BaseMapper<BLL.DTO.PropertyType, PropertyType>
{
    public PropertyTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}