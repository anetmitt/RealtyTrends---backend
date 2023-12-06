using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class TriggerFilterMapper : BaseMapper<BLL.DTO.TriggerFilter, Domain.App.TriggerFilter>
{
    public TriggerFilterMapper(IMapper mapper) : base(mapper)
    {
    }
}