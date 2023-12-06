using BLL.Contracts.Base;
using DAL.Contracts.Base;

namespace BLL.Base;

public abstract class BaseBll<TUow> : IBaseBll
    where TUow : IBaseUow
{
    protected readonly TUow _uow;

    protected BaseBll(TUow uow)
    {
        _uow = uow;
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _uow.SaveChangesAsync();
    }
}