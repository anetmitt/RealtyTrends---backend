using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class FilterTypeMapper : BaseMapper<BLL.DTO.FilterType, FilterType>
{
    public FilterTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}