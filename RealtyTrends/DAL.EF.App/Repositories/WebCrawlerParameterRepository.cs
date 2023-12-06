using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;

namespace DAL.EF.App.Repositories;

public class WebCrawlerParameterRepository :
    EFBaseRepository<WebCrawlerParameter, ApplicationDbContext>, IWebCrawlerParameterRepository
{
    public WebCrawlerParameterRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}