namespace Infrastructure.WebCrawler;

public class RealEstateData
{
    public string Id { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string PricePerUnit { get; set; } = default!;
    public string RoomCount { get; set; } = default!;
    public string PropertySize { get; set; } = default!;
    public string CountyName { get; set; } = default!;
    public string ParishName { get; set; } = default!;
    public string CityName { get; set; } = default!;
    public string StreetName { get; set; } = default!;
    public string EnergyCertificateType { get; set; } = default!;
    public string UnitType { get; set; } = default!;
    
    public string TransactionType { get; set; } = default!;

    public override string ToString()
    {
        return $"Id: {Id}, Price: {Price}, PricePerUnit: {PricePerUnit},\n RoomCount: {RoomCount}," +
               $"PropertySize: {PropertySize}, CountyName: {CountyName},\n  ParishName: {ParishName}," +
               $"CityName: {CityName}, StreetName: {StreetName},\n EnergyCertificateType: {EnergyCertificateType}," +
               $"UnitType: {UnitType}, TransactionType: {TransactionType}\n";
    }
}