using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class WriteTriggerMapper : BaseMapper<Domain.App.Trigger, WriteTrigger>
{
    public WriteTriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}