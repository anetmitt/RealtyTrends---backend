using BLL.Contracts.App;
using BLL.Contracts.Base;
using DAL.Contracts.App;
using Domain.App;
using Public.DTO.v1;

namespace BLL.App.Services;

public class PriceStatisticsService : IPriceStatisticService, IService
{
    private readonly IAppUow _uow;
    
    public PriceStatisticsService(IAppUow uow)
    {
        _uow = uow;
    }

    public async Task<List<VirtualStatisticsData>> GetPriceStatistics(StatisticFilters  filters)
    {
        return await _uow.PriceStatisticsRepository.GetFilteredProperties(filters);
    }
}