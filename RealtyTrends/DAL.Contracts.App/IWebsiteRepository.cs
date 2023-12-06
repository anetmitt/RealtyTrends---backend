using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IWebsiteRepository : IBaseRepository<Website>
{
    public Task<Website?> FindByWebsiteNameAsync(string name);
}