using Domain.Base;

namespace BLL.DTO;

public class FilterType : DomainEntityId
{
    public string Name { get; set; } = default!;
}