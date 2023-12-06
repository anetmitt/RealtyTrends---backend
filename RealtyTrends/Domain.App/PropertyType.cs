using Domain.Base;

namespace Domain.App;

public class PropertyType : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Property>? Properties { get; set; }
}