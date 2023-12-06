using Domain.Base;

namespace Domain.App;

public class TransactionType : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<RegionProperty>? RegionProperties { get; set; }
}