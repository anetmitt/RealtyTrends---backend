using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class UserTrigger : DomainEntityId
{
    public Guid TriggerId { get; set; }
    public Trigger? Trigger { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}