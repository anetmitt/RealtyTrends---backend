using Domain.Base;

namespace Domain.App;

public class PropertyStructure : DomainEntityId
{
    public string Value { get; set; } = default!;
    
    public DateOnly AddedTime { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public Guid PropertyId  { get; set; }
    public Property? Property { get; set; }
    
    public Guid PropertyFieldId { get; set; }
    
    public PropertyField? PropertyField { get; set; }
}