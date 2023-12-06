using Public.DTO.v1;
using StatisticFilters = Public.DTO.v1.StatisticFilters;

namespace WebApp.Models;

/// <summary>
/// Model for Price Statistics page view
/// </summary>
public class PriceStatisticsModel
{
    /// <summary>
    /// List of regions in the country
    /// </summary>
    public IEnumerable<Region>? Counties { get; set; }
    
    /// <summary>
    ///  List of Snapshot years
    /// </summary>
    public List<int> Years { get; set; } = new();
    
    /// <summary>
    /// List of property types
    /// </summary>
    public IEnumerable<PropertyType>? PropertyTypes { get; set; }
    
    /// <summary>
    /// List of transaction types
    /// </summary>
    public IEnumerable<TransactionType>? TransactionTypes { get; set; }
}