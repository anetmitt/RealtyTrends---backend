using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IFilterTypeRepository : IBaseRepository<FilterType>
{
    public Task<FilterType?> GetFilterTypeByName(string name);
}