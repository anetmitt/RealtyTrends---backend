using Domain.Base;

namespace Domain.App;

public class Region : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public Guid RegionTypeId { get; set; }
    public RegionType? RegionType { get; set; }
    
    public Guid? ParentId { get; set; }
    public Region? Parent { get; set; }
    
    public ICollection<Region>? ChildRegions { get; set; }

}