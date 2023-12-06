using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPropertyTypeRepository : IBaseRepository<PropertyType>, IPropertyTypeRepositoryCustom<PropertyType>
{
}

public interface IPropertyTypeRepositoryCustom<TEntity>
{
    public Task<TEntity?> FindByNameAsync(string name);
}