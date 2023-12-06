using Domain.App;
using Public.DTO.v1;

namespace BLL.Contracts.App;

public interface IPriceStatisticService
{
    public Task<List<VirtualStatisticsData>>  GetPriceStatistics(StatisticFilters  filters);
}