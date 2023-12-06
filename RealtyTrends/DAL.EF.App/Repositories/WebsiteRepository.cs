using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class WebsiteRepository :
    EFBaseRepository<Website, ApplicationDbContext>, IWebsiteRepository
{
    public WebsiteRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<Website?> FindByWebsiteNameAsync(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}