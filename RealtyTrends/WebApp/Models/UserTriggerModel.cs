using Public.DTO.v1;

namespace WebApp.Models;

/// <summary>
/// Model for User Triggers page view
/// </summary>
public class UserTriggerModel
{
    /// <summary>
    /// List of User triggers
    /// </summary>
    public List<Trigger?> Triggers { get; set; } = new();
    
    /// <summary>
    /// List of regions in the country
    /// </summary>
    public IEnumerable<Region>? Counties { get; set; }
    
    /// <summary>
    /// List of property types
    /// </summary>
    public IEnumerable<PropertyType>? PropertyTypes { get; set; }

    /// <summary>
    /// List of transaction types
    /// </summary>
    public IEnumerable<TransactionType>? TransactionTypes { get; set; }
}