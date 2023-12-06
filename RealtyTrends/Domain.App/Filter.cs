using Domain.Base;

namespace Domain.App;

public class Filter : DomainEntityId
{
    public float? Value { get; set; }
    
    public Guid? RegionId { get; set; }
    public Region? Region { get; set; }
    
    public Guid? TransactionTypeId { get; set; }
    public TransactionType? TransactionType { get; set; }
    
    public Guid? PropertyTypeId { get; set; }
    public PropertyType? PropertyType { get; set; }
    
    public Guid FilterTypeId { get; set; }
    public FilterType? FilterType { get; set; }
    
    public ICollection<TriggerFilter>? TriggerFilters { get; set; }
}