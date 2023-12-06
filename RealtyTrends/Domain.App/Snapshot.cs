using Domain.Base;

namespace Domain.App;

public class Snapshot : DomainEntityId
{
    public DateOnly CreatedAt { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public ICollection<PropertyUpdate>? PropertyUpdates { get; set; }
}