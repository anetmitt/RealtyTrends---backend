using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class WriteTriggerMapper : BaseMapper<BLL.DTO.WriteTrigger, v1.WriteTrigger>
{
    public WriteTriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}