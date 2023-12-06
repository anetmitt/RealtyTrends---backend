using Domain.Base;

namespace BLL.DTO;

public class RegionType: DomainEntityId
{
    public string Name { get; set; } = default!;
}