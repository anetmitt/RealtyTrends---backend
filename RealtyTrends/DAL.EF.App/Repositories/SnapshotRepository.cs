using DAL.Contracts.App;
using DAL.EF.Base;
using Microsoft.EntityFrameworkCore;
using Snapshot = Domain.App.Snapshot;

namespace DAL.EF.App.Repositories;

public class SnapshotRepository : EFBaseRepository<Snapshot, ApplicationDbContext>, ISnapshotRepository
{
    public SnapshotRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<Snapshot?> GetSnapshotByDateAsync(DateOnly date)
    {
        return await RepositoryDbSet
            .FirstOrDefaultAsync(s => s.CreatedAt == date);
    }
    
}