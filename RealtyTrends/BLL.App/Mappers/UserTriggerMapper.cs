using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class UserTriggerMapper : BaseMapper<UserTrigger, Domain.App.UserTrigger>
{
    public UserTriggerMapper(IMapper mapper) : base(mapper)
    {
    }
}