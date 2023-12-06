using Domain.Base;

namespace Domain.App;

public class RegionProperty : DomainEntityId
{
    public Guid PropertyId { get; set; }
    public Property? Property { get; set; }
    
    public Guid RegionId { get; set; }
    public Region? Region { get; set; }
    
    public Guid TransactionTypeId { get; set; }
    public TransactionType? TransactionType { get; set; }
}