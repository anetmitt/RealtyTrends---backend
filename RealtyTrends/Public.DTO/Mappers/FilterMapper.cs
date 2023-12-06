using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class FilterMapper : BaseMapper<BLL.DTO.Filter, v1.Filter>
{
    public FilterMapper(IMapper mapper) : base(mapper)
    {
    }
}