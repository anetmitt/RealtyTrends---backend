using Domain.Base;


namespace BLL.DTO;

public class Trigger : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public float BeginningSquareMeterPrice { get; set; }
    
    public float UserSquareMeterPrice { get; set; }
    
    public float? CurrentSquareMeterPrice { get; set; }

    public DateOnly TriggerBirthDate { get; set; }
    
    public ICollection<TriggerFilter>? TriggerFilters { get; set; }
    
}