using DAL.Contracts.Base;

namespace DAL.Contracts.App;

public interface IAppUow : IBaseUow
{
    IFilterRepository FilterRepository { get; }
    IFilterTypeRepository FilterTypeRepository { get; }
    IPropertyFieldRepository PropertyFieldRepository { get; }
    IPropertyRepository PropertyRepository { get; }
    
    IPropertyUpdateRepository PropertyUpdateRepository { get; }
    IPropertyStructureRepository PropertyStructureRepository { get; }
    IPropertyTypeRepository PropertyTypeRepository { get; }
    IRegionPropertyRepository RegionPropertyRepository { get; }
    IRegionRepository RegionRepository { get; }
    IRegionTypeRepository RegionTypeRepository { get; }
    ITransactionTypeRepository TransactionTypeRepository { get; }
    ITriggerFilterRepository TriggerFilterRepository { get; }
    ITriggerRepository TriggerRepository { get; }
    IUserTriggerRepository UserTriggerRepository { get; }
    IWebCrawlerParameterRepository WebCrawlerParameterRepository { get; }
    IWebCrawlerRepository WebCrawlerRepository { get; }
    IWebsiteRepository WebsiteRepository { get; }
    ISnapshotRepository SnapshotRepository { get; }
    
    IPriceStatisticsRepository PriceStatisticsRepository { get; }
}