using Domain.Base;

namespace BLL.DTO;

public class WriteTrigger : DomainEntityId
{
    public Guid Id { get; set; }
    public float UserSquareMeterPrice { get; set; }
}