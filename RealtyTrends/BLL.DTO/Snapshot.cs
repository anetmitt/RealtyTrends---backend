using Domain.App;
using Domain.Base;

namespace BLL.DTO;

public class Snapshot : DomainEntityId
{
    public DateOnly CreatedAt { get; set; }
}