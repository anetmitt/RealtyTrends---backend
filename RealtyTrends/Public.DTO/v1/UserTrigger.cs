using BLL.DTO;
using Domain.App;
using Domain.Base;

namespace Public.DTO.v1;

public class UserTrigger : DomainEntityId
{
    public Guid TriggerId { get; set; }
}