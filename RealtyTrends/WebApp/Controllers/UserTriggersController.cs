using System.Text.Json;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Helpers.Base;
using Microsoft.AspNetCore.Authorization;
using Public.DTO.Mappers;
using Public.DTO.v1;
using WebApp.Models;
#pragma warning disable 1591

namespace WebApp.Controllers
{
    [Authorize]
    public class UserTriggersController : Controller
    {
        private readonly IAppBll _bll;
        private readonly TriggerMapper _mapper;
        private readonly RegionMapper _regionMapper;
        private readonly  PropertyTypeMapper _propertyTypeMapper;

        public UserTriggersController(IAppBll bll, IMapper autoMapper)
        {
            _bll = bll;
            _mapper = new TriggerMapper(autoMapper);
            _regionMapper = new RegionMapper(autoMapper);
            _propertyTypeMapper = new PropertyTypeMapper(autoMapper);
        }

        // GET: UserTriggers
        public async Task<IActionResult> Index()
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
            
            var triggerModel = new UserTriggerModel()
            {
                Triggers = triggerDtos!,
                Counties = await GetCounties(),
                PropertyTypes = await GetPropertyTypes()
            };

            return View(triggerModel);
        }

      
        /// <summary>
        /// Used to get all counties from the database and display them as selectable options in the view
        /// </summary>
        /// <returns>Returns a list of Region objects which are counties</returns>
        public async Task<IEnumerable<Public.DTO.v1.Region>> GetCounties()
        {
            var counties = (await _bll.RegionsService.AllCountiesAsync())
                .Select(e => _regionMapper.Map(e));
            return counties!;
        }

        /// <summary>
        /// Used to get all property types from the database and display them as selectable options in the view
        /// </summary>
        /// <returns>Returns a list of PropertyType objects</returns>
        public async Task<IEnumerable<Public.DTO.v1.PropertyType>> GetPropertyTypes()
        {
            var propertyTypes = (await _bll.PropertyTypesService.AllAsync())
                .Select(e => _propertyTypeMapper.Map(e));
            return propertyTypes!;
        }

  
        /// <summary>
        /// Used to get all Regions, which parent region's id is the give id, from the database and display
        /// them as selectable options in the view
        /// </summary>
        /// <param name="Id">Region parent id</param>
        /// <returns>array of region objects which are serialized to JSON</returns>
        public async Task<string> GetParentRegion(Guid Id)
        {
            var parishes = (await _bll.RegionsService.AllByParentIdAsync(Id))
                .Select(e => _regionMapper.Map(e));
            return JsonSerializer.Serialize(parishes);

        }

        //[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTrigger(Guid id)
        {
            await _bll.UserTriggersService.RemoveAsync(id, User.GetUserId());
            return Ok();
        }

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
