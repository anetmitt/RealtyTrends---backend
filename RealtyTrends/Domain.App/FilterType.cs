using Domain.Base;

namespace Domain.App;

public class FilterType : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Filter>? Filters { get; set; }
}