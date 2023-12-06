using Domain.Base;

namespace Domain.App;

public class PropertyUpdate : DomainEntityId
{
    public Guid PropertyId { get; set; }
    public Property? Property { get; set; }
    
    public Guid SnapshotId { get; set; }
    
    public Snapshot? Snapshot { get; set; }
}