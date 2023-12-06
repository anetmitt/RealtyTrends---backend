using AutoMapper;
using BLL.App.Mappers;
using BLL.App.Services;
using BLL.Base;
using BLL.Contracts.App;
using DAL.Contracts.App;

namespace BLL.App;

public class AppBll : BaseBll<IAppUow>, IAppBll
{
    protected IAppUow Uow;
    private readonly IMapper _mapper;

    public AppBll(IAppUow uow, IMapper mapper) : base(uow)
    {
        Uow = uow;
        _mapper = mapper;
    }

    private ITriggerFiltersService? _triggerFilters;
    
    public ITriggerFiltersService TriggerFiltersService =>
        _triggerFilters ??= new TriggerFiltersService(new TriggerFilterMapper(_mapper), Uow);

    private ITriggersService? _triggers;
    
    public ITriggersService TriggersService =>
        _triggers ??= new TriggersService(new TriggerMapper(_mapper), Uow);
    
    private IUserTriggerService? _userTriggers;
    
    public IUserTriggerService UserTriggersService =>
        _userTriggers ??= new UserTriggerService(new UserTriggerMapper(_mapper), Uow);
    
    private IRegionService? _regions;
    
    public IRegionService RegionsService =>
        _regions ??= new RegionService(new RegionMapper(_mapper), Uow);
    
    public IPriceStatisticService? _priceStatistics;
    
    public IPriceStatisticService PriceStatisticsService =>
        _priceStatistics ??= new PriceStatisticsService(Uow);

    public IPropertyTypeService? _propertyTypes;
    
    public IPropertyTypeService PropertyTypesService =>
        _propertyTypes ??= new PropertyTypeService(new PropertyTypeMapper(_mapper), Uow);
    
    public ISnapshotService? _snapshots;
    
    public ISnapshotService SnapshotsService =>
        _snapshots ??= new SnapshotService(new SnapshotMapper(_mapper), Uow);
    
    public ITransactionTypeService? _transactionTypes;
    
    public ITransactionTypeService TransactionTypesService =>
        _transactionTypes ??= new TransactionTypeService(new TransactionTypeMapper(_mapper), Uow);
}