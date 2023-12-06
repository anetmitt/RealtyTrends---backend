using Asp.Versioning;
using BLL.App.Services;
using DAL.Contracts.App;
using DAL.EF.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers;

/// <summary>
/// API Controller for updating the real estate data
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize(Roles="admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class UpdateRealEstateData : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IAppUow _uow;

    /// <summary>
    /// Constructor for UpdateRealEstateData
    /// </summary>
    /// <param name="context"></param>
    /// <param name="uow"></param>
    public UpdateRealEstateData(ApplicationDbContext context, IAppUow uow)
    {
        _context = context;
        _uow = uow;
    }
    
    /// <summary>
    /// Updates the real estate data or adds new data if it doesn't exist
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult> UpdateData()
    {
        WebCrawlerService webCrawlerService = new(_uow);
        await webCrawlerService.CrawlAndStoreData();
        return Ok();
    }
}