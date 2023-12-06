using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class SnapshotService: BaseEntityService<BLL.DTO.Snapshot, Snapshot, ISnapshotRepository>, ISnapshotService
{
    
    protected readonly IAppUow Uow;
    
    public SnapshotService(IMapper<DTO.Snapshot, Snapshot> mapper, IAppUow uow) : base(uow.SnapshotRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.Snapshot?> GetSnapshotByDateAsync(DateOnly date)
    {
        return Mapper.Map(await Uow.SnapshotRepository.GetSnapshotByDateAsync(date));
    }

    public async Task<IEnumerable<DTO.Snapshot>> AllAsync()
    {
        return (await Uow.SnapshotRepository.AllAsync()).Select(e => Mapper.Map(e));
    }
}