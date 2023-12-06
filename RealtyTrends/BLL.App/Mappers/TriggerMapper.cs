
using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class TriggerMapper : BaseMapper<BLL.DTO.Trigger, Domain.App.Trigger>
{
    public TriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}