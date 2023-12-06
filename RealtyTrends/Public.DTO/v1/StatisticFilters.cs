using System.Text.Json.Serialization;

namespace Public.DTO.v1;

public class StatisticFilters
{
    public Guid CountySelect { get; set; }
    public Guid? ParishSelect { get; set; }
    public Guid? CitySelect { get; set; }
    public Guid? StreetSelect { get; set; }
    public Guid TransactionType { get; set; }
    public Guid PropertyType { get; set; }
    public int? RoomsCountMin { get; set; }
    public int? RoomsCountMax { get; set; }
    public int? PricePerUnitMax { get; set; }
    public int? PricePerUnitMin { get; set; }
    public int? AreaMin { get; set; }
    public int? AreaMax { get; set; }
    
    public float TriggerPricePerUnit { get; set; } = default!;

    public string TriggerName { get; set; } = "";
    
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly StartDate { get; set; }
    
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly? EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}