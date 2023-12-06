namespace Public.DTO.v1;

public class Filter
{
    public float? Value { get; set; }
    public Region? Region { get; set; }
    public TransactionType? TransactionType { get; set; }
    public PropertyType? PropertyType { get; set; }
    public FilterType? FilterType { get; set; }
}