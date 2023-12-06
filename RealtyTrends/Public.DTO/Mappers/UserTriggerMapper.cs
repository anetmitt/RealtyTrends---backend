using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class UserTriggerMapper: BaseMapper<BLL.DTO.UserTrigger, v1.UserTrigger>
{
    public UserTriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}