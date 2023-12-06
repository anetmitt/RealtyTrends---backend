using Domain.App;
using Public.DTO.v1;

namespace DAL.Contracts.App;

public interface IPriceStatisticsRepository
{
    public Task<List<VirtualStatisticsData>> GetFilteredProperties(StatisticFilters filters);
}