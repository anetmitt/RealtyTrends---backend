using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class PropertyFieldRepository :
    EFBaseRepository<PropertyField, ApplicationDbContext>, IPropertyFieldRepository
{
    public PropertyFieldRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<PropertyField?> FindByNameAsync(string name)
    {
        return await RepositoryDbSet.FirstOrDefaultAsync(x => x.Name == name);
    }
}