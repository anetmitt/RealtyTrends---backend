using BLL.Contracts.Base;

namespace BLL.Contracts.App;

public interface IAppBll : IBaseBll
{
    IUserTriggerService UserTriggersService { get; }
    ITriggersService TriggersService { get; }
    ITriggerFiltersService TriggerFiltersService { get; }
    
    IRegionService RegionsService { get; }
    
    IPriceStatisticService PriceStatisticsService { get; }
    
    IPropertyTypeService PropertyTypesService { get; }
    
    ISnapshotService SnapshotsService { get; }
    
    ITransactionTypeService TransactionTypesService { get; }
}