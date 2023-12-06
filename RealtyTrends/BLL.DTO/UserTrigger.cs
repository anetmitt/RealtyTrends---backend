using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace BLL.DTO;

public class UserTrigger : DomainEntityId
{
    public Guid TriggerId { get; set; }
    public Guid AppUserId { get; set; }
}