using DAL.Contracts.App;
using DAL.EF.Base;
using Domain.App;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.App.Repositories;

public class UserTriggerRepository :
    EFBaseRepository<UserTrigger, ApplicationDbContext>, IUserTriggerRepository
{
    public UserTriggerRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<UserTrigger>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.Trigger)
            .ToListAsync();
    }

    public override async Task<UserTrigger?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.Trigger)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<IEnumerable<UserTrigger>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(u => u.Trigger)
            .Where(u => u.AppUserId == userId)
            .ToListAsync();
    }

    public virtual async Task UpdateTriggerAsync(Guid id, Guid userId,  float ppu)
    {
        var userTrigger = await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.Trigger)
            .FirstOrDefaultAsync(m => m.TriggerId == id && m.AppUserId == userId);
        if (userTrigger == null) return;
        
        var trigger = userTrigger.Trigger!;
        trigger.UserSquareMeterPrice = ppu;
        RepositoryDbContext.Triggers.Update(trigger);
    }

    public virtual async Task<UserTrigger?> FindAsync(Guid triggerId, Guid userId)
    {
        return await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.Trigger)
            .FirstOrDefaultAsync(m => m.TriggerId == triggerId && m.AppUserId == userId);
    }

    public virtual async Task<UserTrigger?> RemoveAsync(Guid id, Guid userId)
    {
        var userTrigger = await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.Trigger)
            .FirstOrDefaultAsync(m => m.TriggerId == id && m.AppUserId == userId);
        if (userTrigger == null) return null;
        
        RepositoryDbSet.Remove(userTrigger);
        RepositoryDbContext.Triggers.Remove(userTrigger.Trigger!);
        
        return userTrigger;
    }
    
    public virtual void Add(UserTrigger userTrigger)
    {
        RepositoryDbSet.Add(userTrigger);
    }
}