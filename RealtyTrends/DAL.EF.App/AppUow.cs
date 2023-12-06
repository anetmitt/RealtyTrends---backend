using DAL.Contracts.App;
using DAL.EF.App.Repositories;
using DAL.EF.Base;

namespace DAL.EF.App;

public class AppUow : EFBaseUow<ApplicationDbContext>, IAppUow
{
    private IFilterRepository? _filterRepository;
    private IFilterTypeRepository? _filterTypeRepository;
    private IPropertyFieldRepository? _propertyFieldRepository;
    private IPropertyRepository? _propertyRepository;
    private IPropertyUpdateRepository? _propertyUpdateRepository;
    private IPropertyStructureRepository? _propertyStructureRepository;
    private IPropertyTypeRepository? _propertyTypeRepository;
    private IRegionPropertyRepository? _regionPropertyRepository;
    private IRegionRepository? _regionRepository;
    private IRegionTypeRepository? _regionTypeRepository;
    private ITransactionTypeRepository? _transactionTypeRepository;
    private ITriggerFilterRepository? _triggerFilterRepository;
    private ITriggerRepository? _triggerRepository;
    private IUserTriggerRepository? _userTriggerRepository;
    private IWebCrawlerParameterRepository? _webCrawlerParameterRepository;
    private IWebCrawlerRepository? _webCrawlerRepository;
    private IWebsiteRepository? _websiteRepository;
    private ISnapshotRepository? _snapshotRepository;
    private IPriceStatisticsRepository? _priceStatisticsRepository;

    public AppUow(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public IFilterRepository FilterRepository => 
        _filterRepository ??= new FilterRepository(UowDbContext);
    
    public IFilterTypeRepository FilterTypeRepository =>
        _filterTypeRepository ??= new FilterTypeRepository(UowDbContext);

    public IPropertyFieldRepository PropertyFieldRepository =>
        _propertyFieldRepository ??= new PropertyFieldRepository(UowDbContext);
    
    public IPropertyRepository PropertyRepository =>
        _propertyRepository ??= new PropertyRepository(UowDbContext);

    public IPropertyUpdateRepository PropertyUpdateRepository =>
        _propertyUpdateRepository ??= new PropertyUpdateRepository(UowDbContext);

    public IPropertyStructureRepository PropertyStructureRepository =>
        _propertyStructureRepository ??= new PropertyStructureRepository(UowDbContext);
    
    public IPropertyTypeRepository PropertyTypeRepository =>
        _propertyTypeRepository ??= new PropertyTypeRepository(UowDbContext);
    
    public IRegionPropertyRepository RegionPropertyRepository =>
        _regionPropertyRepository ??= new RegionPropertyRepository(UowDbContext);
    
    public IRegionRepository RegionRepository =>
        _regionRepository ??= new RegionRepository(UowDbContext);
    
    public IRegionTypeRepository RegionTypeRepository =>
        _regionTypeRepository ??= new RegionTypeRepository(UowDbContext);
    
    public ITransactionTypeRepository TransactionTypeRepository =>
        _transactionTypeRepository ??= new TransactionTypeRepository(UowDbContext);
    
    public ITriggerFilterRepository TriggerFilterRepository =>
        _triggerFilterRepository ??= new TriggerFilterRepository(UowDbContext);
    
    public ITriggerRepository TriggerRepository =>
        _triggerRepository ??= new TriggerRepository(UowDbContext);
    
    public IUserTriggerRepository UserTriggerRepository =>
        _userTriggerRepository ??= new UserTriggerRepository(UowDbContext);
    
    public IWebCrawlerParameterRepository WebCrawlerParameterRepository =>
        _webCrawlerParameterRepository ??= new WebCrawlerParameterRepository(UowDbContext);
    
    public IWebCrawlerRepository WebCrawlerRepository =>
        _webCrawlerRepository ??= new WebCrawlerRepository(UowDbContext);
    
    public IWebsiteRepository WebsiteRepository =>
        _websiteRepository ??= new WebsiteRepository(UowDbContext);
    
    public ISnapshotRepository SnapshotRepository =>
        _snapshotRepository ??= new SnapshotRepository(UowDbContext);
    
    public IPriceStatisticsRepository PriceStatisticsRepository =>
        _priceStatisticsRepository ??= new PriceStatisticsRepository(UowDbContext);
    
}