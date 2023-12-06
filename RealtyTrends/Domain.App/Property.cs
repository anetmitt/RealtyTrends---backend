using Domain.Base;

namespace Domain.App;

public class Property : DomainEntityId
{
    public int ExternalId { get; set; }

    public Guid PropertyTypeId { get; set; }
    public PropertyType? PropertyType { get; set; }
    
    public Guid WebsiteId { get; set; }
    public Website? Website { get; set; }
    
    public ICollection<PropertyStructure>? PropertyStructures { get; set; }
    
    public ICollection<RegionProperty>? RegionProperties { get; set; }
    
    public ICollection<PropertyUpdate>? PropertyUpdates { get; set; }
}