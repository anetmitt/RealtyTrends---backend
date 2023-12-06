using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class StatisticFilterMapper : BaseMapper<BLL.DTO.StatisticFilters, v1.StatisticFilters>
{
    public StatisticFilterMapper(IMapper mapper) : base(mapper)
    {
    }
}