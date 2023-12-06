using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IRegionTypeRepository : IBaseRepository<RegionType>
{
    public Task<RegionType?> FindByNameAsync(string name);
}