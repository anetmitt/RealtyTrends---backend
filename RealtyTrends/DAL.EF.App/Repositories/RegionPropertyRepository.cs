using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class RegionPropertyRepository :
    EFBaseRepository<RegionProperty, ApplicationDbContext>, IRegionPropertyRepository
{
    public RegionPropertyRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<RegionProperty>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(r => r.Property)
            .Include(r => r.Region)
            .Include(r => r.TransactionType)
            .ToListAsync();
    }

    public override async Task<RegionProperty?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(r => r.Property)
            .Include(r => r.Region)
            .Include(r => r.TransactionType)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<List<Guid>> GetPropertiesByRegionAndTransactionTypeAsync(Guid regionId, 
        Guid transactionTypeId, List<Guid> propertiesInDateRange)
    {
        return await RepositoryDbSet
            .Include(r => r.Property)
            .Include(r => r.Region)
            .Include(r => r.TransactionType)
            .Where(r => r.RegionId == regionId && r.TransactionType!.Id == transactionTypeId
                                               && propertiesInDateRange.Contains(r.Property!.Id))
            .Select(pu => pu.PropertyId) 
            .ToListAsync();
    }



}