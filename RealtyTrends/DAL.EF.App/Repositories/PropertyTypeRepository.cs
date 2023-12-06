using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PropertyTypeRepository :
    EFBaseRepository<PropertyType, ApplicationDbContext>, IPropertyTypeRepository
{
    public PropertyTypeRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<PropertyType?> FindByNameAsync(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}