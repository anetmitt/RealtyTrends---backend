using Domain.App;
using Domain.Base;

namespace BLL.DTO;

public class PropertyType : DomainEntityId
{
    public string Name { get; set; } = default!;
}