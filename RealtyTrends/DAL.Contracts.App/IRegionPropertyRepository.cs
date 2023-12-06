using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IRegionPropertyRepository : IBaseRepository<RegionProperty>
{
    Task<List<Guid>> GetPropertiesByRegionAndTransactionTypeAsync(Guid regionId, Guid transactionTypeId,
        List<Guid> propertiesInDateRange);
}