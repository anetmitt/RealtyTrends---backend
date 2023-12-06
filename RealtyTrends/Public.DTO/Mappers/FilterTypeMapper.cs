using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class FilterTypeMapper : BaseMapper<BLL.DTO.FilterType, v1.FilterType>
{
    public FilterTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}