using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class WebCrawlerRepository :
    EFBaseRepository<WebCrawler, ApplicationDbContext>, IWebCrawlerRepository
{
    public WebCrawlerRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<WebCrawler>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(w => w.WebCrawlerParameter)
            .Include(w => w.Website)
            .ToListAsync();
    }

    public override async Task<WebCrawler?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(w => w.WebCrawlerParameter)
            .Include(w => w.Website)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}