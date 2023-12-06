using Domain.Base;

namespace Domain.App;

public class PropertyField : DomainEntityId
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    
    public ICollection<PropertyStructure>? PropertyStructures { get; set; }
}