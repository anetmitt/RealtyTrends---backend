
namespace Public.DTO.v1;

public class Trigger
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = default!;
    
    public float BeginningSquareMeterPrice { get; set; }
    
    public float UserSquareMeterPrice { get; set; }
    
    public float? CurrentSquareMeterPrice { get; set; }

    public DateOnly TriggerBirthDate { get; set; }
    
    public ICollection<TriggerFilter>? TriggerFilters { get; set; }
}