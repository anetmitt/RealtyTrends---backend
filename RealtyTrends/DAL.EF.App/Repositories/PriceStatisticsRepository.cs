using System.Text;
using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;
using Public.DTO.v1;

namespace DAL.EF.App.Repositories;

public class PriceStatisticsRepository : EFBaseRepository<Property, ApplicationDbContext>, IPriceStatisticsRepository
{
    
    public PriceStatisticsRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<List<VirtualStatisticsData>> GetFilteredProperties(StatisticFilters filters)
    {
        string path = @"C:\Users\anetm\icd0021-22-23-s\RealtyTrends\DAL.EF.App\Queries\StatisticsQuery.sql"; // change to your file path
        string query = File.ReadAllText(path);
    
        var queryBuilder = new StringBuilder(query);

        // Replace the parameter placeholders in the query with actual parameter values
        queryBuilder.Replace("@CountySelect", $"'{filters.CountySelect}'")
            .Replace("@ParishSelect", filters.ParishSelect != null ? $"'{filters.ParishSelect}'" : "NULL")
            .Replace("@CitySelect", filters.CitySelect != null ? $"'{filters.CitySelect}'" : "NULL")
            .Replace("@StreetSelect", filters.StreetSelect != null ? $"'{filters.StreetSelect}'" : "NULL")
            .Replace("@TransactionType", $"'{filters.TransactionType}'")
            .Replace("@PropertyType", $"'{filters.PropertyType}'")
            .Replace("@RoomsCountMin", filters.RoomsCountMin?.ToString() ?? "NULL")
            .Replace("@RoomsCountMax", filters.RoomsCountMax?.ToString() ?? "NULL")
            .Replace("@PricePerUnitMin", filters.PricePerUnitMin?.ToString() ?? "NULL")
            .Replace("@PricePerUnitMax", filters.PricePerUnitMax?.ToString() ?? "NULL")
            .Replace("@AreaMin", filters.AreaMin?.ToString() ?? "NULL")
            .Replace("@AreaMax", filters.AreaMax?.ToString() ?? "NULL")
            .Replace("@StartDate", $"'{filters.StartDate:yyyy-MM-dd}'")
            .Replace("@EndDate", $"'{filters.EndDate:yyyy-MM-dd}'");


        string queryWithValues = queryBuilder.ToString();
        
        var result = await RepositoryDbContext.VirtualStatisticsData
            .FromSqlRaw(queryWithValues)
            .ToListAsync();

        Console.WriteLine("Result count: " + result.Count);
        return result;
    }

    
}