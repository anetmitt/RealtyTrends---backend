namespace BLL.DTO;

public class StatisticFilters
{
    public Guid? CountySelect { get; set; }
    
    public Guid? ParishSelect { get; set; }
    
    public Guid? CitySelect { get; set; }
    
    public Guid? StreetSelect { get; set; }

    public string TransactionType { get; set; } = default!;
    
    public string PropertyType { get; set; } = default!;
    
    public int? RoomsCountMin { get; set; }
    
    public int? RoomsCountMax { get; set; }
    
    public int? PricePerUnitMax { get; set; }
    
    public int? PricePerUnitMin { get; set; }
    
    public int? AreaMin { get; set; }
    
    public int? AreaMax { get; set; }

    public DateOnly StartDate { get; set; }
    
    public DateOnly EndDate { get; set; }
}