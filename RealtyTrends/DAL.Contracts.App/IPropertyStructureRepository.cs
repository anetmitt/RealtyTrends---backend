using DAL.Contracts.Base;
using Domain.App;

namespace DAL.Contracts.App;

public interface IPropertyStructureRepository : IBaseRepository<PropertyStructure>
{
    public Task<PropertyStructure?> FindLastPrice(Guid propertyId);

    public Task<IEnumerable<Property>> GetPropertiesByItsFieldsRange(int? minValue, int? maxValue, string fieldName,
        List<Guid> filteredProperties);
}