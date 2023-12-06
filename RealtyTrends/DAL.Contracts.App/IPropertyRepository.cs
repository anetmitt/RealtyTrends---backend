using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPropertyRepository : IBaseRepository<Property>
{
    public Task<Property?> FindByExternalIdAsync(int externalId);
    
    public Property Add(Property property, Guid snapshotId);
}