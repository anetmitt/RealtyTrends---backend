using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class TriggerMapper : BaseMapper<BLL.DTO.Trigger, v1.Trigger>
{
    public TriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}