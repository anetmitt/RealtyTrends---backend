using Domain.Base;

namespace BLL.DTO;

public class TriggerFilter : DomainEntityId
{
    public Filter? Filter { get; set; }
}