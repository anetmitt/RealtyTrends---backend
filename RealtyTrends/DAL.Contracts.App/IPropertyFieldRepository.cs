using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPropertyFieldRepository : IBaseRepository<PropertyField>
{
    public Task<PropertyField?> FindByNameAsync(string name);
}