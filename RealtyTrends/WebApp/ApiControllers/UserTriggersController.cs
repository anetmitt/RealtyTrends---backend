using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Helpers.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using Public.DTO.v1;
using WebApp.Models;
using TriggerMapper = Public.DTO.Mappers.TriggerMapper;

namespace WebApp.ApiControllers
{
    /// <summary>
    /// API Controller for User Triggers
    /// - Gets all user triggers
    /// - Allows you to create a new trigger
    /// - Allows you to delete a trigger
    /// - Allows you to edit a trigger
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserTriggersController : ControllerBase
    {
        private readonly IAppBll _bll;
        private readonly TriggerMapper _mapper;
        private readonly RegionMapper _regionMapper;
        private readonly  PropertyTypeMapper _propertyTypeMapper;
        private readonly TransactionTypeMapper _transactionTypeMapper;

        /// <summary>
        /// Constructor for UserTriggersController
        /// </summary>
        /// <param name="bll"></param>
        /// <param name="autoMapper"></param>
        public UserTriggersController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new TriggerMapper(autoMapper);
            _regionMapper = new RegionMapper(autoMapper);
            _propertyTypeMapper = new PropertyTypeMapper(autoMapper);
            _transactionTypeMapper = new TransactionTypeMapper(autoMapper);
        }

        // GET: api/UserTriggers
        /// <summary>
        /// Gets all user triggers and filter parameters to create the new trigger
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> Index()
        {
            var allUserTriggers =  await _bll.UserTriggersService.AllAsync(User.GetUserId());
            var triggerDtos = new List<Public.DTO.v1.Trigger>();

            foreach (var currentTrigger in allUserTriggers)
            {
                await _bll.TriggerFiltersService.GetAllTriggerFiltersAsync(currentTrigger.TriggerId);
                var triggerDto = _mapper.Map(await _bll.TriggersService.FindAsync(currentTrigger.TriggerId));
                
                if (triggerDto == null) {continue;}
                
                triggerDtos.Add(triggerDto);
            }
            
            var triggerModel = new UserTriggerModel
            {
                Triggers = triggerDtos!,
                Counties = await GetCounties(),
                PropertyTypes = await GetPropertyTypes(),
                TransactionTypes = await GetTransactionTypes()
            };

            return JsonSerializer.Serialize(triggerModel);
        }

         /// <summary>
        /// Used to get all counties from the database and display them as selectable options in the view
        /// </summary>
        /// <returns>Returns a list of Region objects which are counties</returns>
         private async Task<IEnumerable<Region>> GetCounties()
        {
            var counties = (await _bll.RegionsService.AllCountiesAsync())
                .Select(e => _regionMapper.Map(e));
            return counties!;
        }


         private async Task<IEnumerable<TransactionType>> GetTransactionTypes()
         {
             var transactionTypes = (await _bll.TransactionTypesService.AllAsync())
                 .Select(e => _transactionTypeMapper.Map(e));
             
             return transactionTypes!;
         }

        /// <summary>
        /// Used to get all property types from the database and display them as selectable options in the view
        /// </summary>
        /// <returns>Returns a list of PropertyType objects</returns>
        private async Task<IEnumerable<PropertyType>> GetPropertyTypes()
        {
            var propertyTypes = (await _bll.PropertyTypesService.AllAsync())
                .Select(e => _propertyTypeMapper.Map(e));
            return propertyTypes!;
        }

  
        /// <summary>
        /// Used to get all Regions, which parent region's id is the give id, from the database and display
        /// them as selectable options in the view
        /// </summary>
        /// <param name="id">Region parent id</param>
        /// <returns>array of region objects which are serialized to JSON</returns>
        [HttpGet("{Id}")]
        public async Task<string> GetParentRegion(Guid id)
        {
            var parishes = (await _bll.RegionsService.AllByParentIdAsync(id))
                .Select(e => _regionMapper.Map(e));
            return JsonSerializer.Serialize(parishes);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}/{newPpu}")]
        public async Task<IActionResult> UpdateUserTrigger(Guid id, string newPpU)
        {
            
            var getTrigger = await _bll.UserTriggersService.FindAsync(id, User.GetUserId());
            
            if (getTrigger == null)
            {
                return NotFound();
            }
            
            await _bll.UserTriggersService.UpdateTriggerAsync(id, User.GetUserId(), float.Parse(newPpU));
            
            return Ok();
        }

        /// <summary>
        /// Used to delete the user trigger from the database
        /// </summary>
        /// <param name="id">Id of the user trigger</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTrigger(Guid id)
        {
            await _bll.UserTriggersService.RemoveAsync(id, User.GetUserId());
            return Ok();
        }

        
        /// <summary>
        /// Used to add a new user trigger to the database
        /// </summary>
        /// <param name="filters">Input object of form inputs which are used to creat new trigger</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> NewUserTrigger([FromBody] StatisticFilters filters)
        {
            var currentAvgPrice = Convert.ToSingle((await _bll.PriceStatisticsService.GetPriceStatistics(filters))[0].AveragePricePerUnit);
            await _bll.UserTriggersService.AddNewTriggerToUser(User.GetUserId(), filters, currentAvgPrice);
            return Ok();
        }
    }
}
