using Domain.Base;

namespace BLL.DTO;

public class TransactionType : DomainEntityId
{
    public string Name { get; set; } = default!;
}