using Domain.Base;

namespace Domain.App;

public class TriggerFilter : DomainEntityId
{
    public Guid FilterId { get; set; }
    public Filter? Filter { get; set; }
    
    public Guid TriggerId { get; set; }
    public Trigger? Trigger { get; set; }
}