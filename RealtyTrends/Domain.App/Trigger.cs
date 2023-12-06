using Domain.Base;

namespace Domain.App;

public class Trigger : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public float BeginningSquareMeterPrice { get; set; }
    
    public float UserSquareMeterPrice { get; set; }
    
    public float? CurrentSquareMeterPrice { get; set; }

    public DateOnly TriggerBirthDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    
    public ICollection<TriggerFilter>? TriggerFilters { get; set; }
    
    public ICollection<UserTrigger>? UserTriggers { get; set; }
}