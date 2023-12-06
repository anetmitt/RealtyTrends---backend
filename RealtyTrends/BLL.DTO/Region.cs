using Domain.Base;

namespace BLL.DTO;

public class Region : DomainEntityId
{
    public string Name { get; set; } = default!;
    public Guid? ParentId { get; set; }
}