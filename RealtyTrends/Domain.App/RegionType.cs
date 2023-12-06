using Domain.Base;

namespace Domain.App;

public class RegionType: DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Region>? Regions { get; set; }
}