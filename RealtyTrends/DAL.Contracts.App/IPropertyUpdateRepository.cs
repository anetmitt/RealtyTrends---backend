using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPropertyUpdateRepository : IBaseRepository<PropertyUpdate>
{
    public Task<PropertyUpdate?> FindByPropertyAsync(Guid propertyId);

    public Task<List<Guid>> FindPropertyIdsByDateRangeAsync(DateOnly startDate, DateOnly endDate);
}