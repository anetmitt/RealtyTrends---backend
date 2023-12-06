using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Public.DTO.Mappers;
using Public.DTO.v1;
using WebApp.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApp.ApiControllers;

/// <summary>
/// Controller for Price Statistics
/// Filtering options are set in the view and sent to the controller as a StatisticFilters object
/// Based on filters, the controller returns a list of JSON objects containing the data for the chart
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ApiController]
public class PriceStatisticsController : ControllerBase
{
    private readonly IAppBll _bll;
    private readonly RegionMapper _regionMapper;
    private readonly SnapshotMapper _snapshotsMapper;
    private static IEnumerable<Snapshot>? _snapshots;
    private readonly  StatisticFilterMapper _filterMapper;
    private readonly  PropertyTypeMapper _propertyTypeMapper;
    private readonly TransactionTypeMapper _transactionTypeMapper;


    /// <summary>
    /// Constructor for PriceStatisticsController
    /// </summary>
    /// <param name="bll">BLL</param>
    /// <param name="autoMapper">automapper</param>
    public PriceStatisticsController(IAppBll bll,  IMapper autoMapper)
    {
        _bll = bll;
        _regionMapper = new RegionMapper(autoMapper);
        _snapshotsMapper = new SnapshotMapper(autoMapper);
        _filterMapper = new StatisticFilterMapper(autoMapper);
        _propertyTypeMapper = new PropertyTypeMapper(autoMapper);
        _transactionTypeMapper = new TransactionTypeMapper(autoMapper);
    }

    /// <summary>
    /// Gets me base filters for Price Statistics page
    /// </summary>
    /// <returns>Returns the view for Price Statistics</returns>
    [HttpGet]
    public async Task<string> GetBaseStatisticsFilters()
    {
        var counties = await GetCounties();
        var propertyTypes = await GetPropertyTypes();
        var years = await GetYears();
        
        var dto = new PriceStatisticsModel
        {
            Counties = counties,
            PropertyTypes = propertyTypes,
            Years = years,
            TransactionTypes = await GetTransactionTypes()
        };
        
        return JsonSerializer.Serialize(dto);
    }
    
    private async Task<IEnumerable<TransactionType>> GetTransactionTypes()
    {
        var transactionTypes = (await _bll.TransactionTypesService.AllAsync())
            .Select(e => _transactionTypeMapper.Map(e));
             
        return transactionTypes!;
    }
    
    private async Task<IEnumerable<PropertyType>> GetPropertyTypes()
    {
        var propertyTypes = (await _bll.PropertyTypesService.AllAsync())
            .Select(e => _propertyTypeMapper.Map(e));
        return propertyTypes!;
    }
    
    // GET: api/PriceStatistics
    private async Task<IEnumerable<Region>> GetCounties()
    {
        var counties = (await _bll.RegionsService.AllCountiesAsync())
            .Select(e => _regionMapper.Map(e));
        return counties!;
    }
    
    
    /// <summary>
    /// Used to fetch all Regions, which parent region's id is the give id, from the database and display
    /// them as selectable options in the view
    /// </summary>
    /// <param name="Id">Region parent id</param>
    /// <returns>array of region objects which are serialized to JSON</returns>
    [HttpGet("{Id}")]
    public async Task<string> GetParentRegion(Guid Id)
    {
        var parishes = (await _bll.RegionsService.AllByParentIdAsync(Id))
            .Select(e => _regionMapper.Map(e));
        return JsonSerializer.Serialize(parishes);
    }
    
    private async Task<List<int>> GetYears()
    {
        if (_snapshots == null) { await GetDates(); }
        
        return _snapshots!.Select(d => d.CreatedAt.Year).Distinct().ToList();
    }
    
    /// <summary>
    /// Used to get all selected year snapshot months from the database and display them as selectable options in the view
    /// </summary>
    /// <param name="year">year</param>
    /// <returns>List of months</returns>
    [HttpGet("{year}")]
    public async Task<List<int>> GetSelectedYearMonths(int year)
    {
        if (_snapshots == null) { await GetDates(); }
        
        return _snapshots!.Where(d => d.CreatedAt.Year == year).Select(d => d.CreatedAt.Month).Distinct().ToList();
    }
    
    /// <summary>
    /// Used to get all selected year and month snapshot days from the database and display them as selectable options in the view
    /// </summary>
    /// <param name="year">Snapshot year</param>
    /// <param name="month">Snapshot month</param>
    /// <returns>List of days</returns>
    [HttpGet("{year:int}/{month:int}")]
    public async Task<List<int>> GetSelectedMonthDays(int year, int month)
    {
        if (_snapshots == null) { await GetDates(); }
        
        return _snapshots!.Where(d => d.CreatedAt.Year == year && d.CreatedAt.Month == month)
            .Select(d => d.CreatedAt.Day).Distinct().ToList();
    }
    
    private async Task GetDates()
    {
        _snapshots = (await _bll.SnapshotsService.AllAsync()).Select(e => _snapshotsMapper.Map(e))!;
    }
    
    
    /// <summary>
    ///  Fetches list of objects containing data about dates and price per units for the chart based on the filters
    /// </summary>
    /// <param name="filters">selected filters</param>
    /// <returns>Json of list of objects containing data about dates and price per units</returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<string> GetPriceStatistics([FromBody] StatisticFilters filters)
    {
        var result = (await _bll.PriceStatisticsService.GetPriceStatistics(filters))
            ;
        return JsonSerializer.Serialize(result);
    }
}