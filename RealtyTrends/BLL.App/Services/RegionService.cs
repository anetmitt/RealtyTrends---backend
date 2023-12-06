using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class RegionService: BaseEntityService<BLL.DTO.Region, Region, IRegionRepository>, IRegionService
{
    protected readonly IAppUow Uow;
    
    public RegionService(IMapper<BLL.DTO.Region, Region> mapper, IAppUow uow) : base(uow.RegionRepository, mapper)
    {
        Uow = uow;
    }

    public async Task<DTO.Region?> FindByNameAsync(string name)
    {
        return Mapper.Map(await Uow.RegionRepository.FindByNameAsync(name));
    }

    public async Task<IEnumerable<DTO.Region>> AllByParentIdAsync(Guid parentId)
    {
        return (await Uow.RegionRepository.AllByParentIdAsync(parentId)).Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<DTO.Region>> AllCountiesAsync()
    {
        return (await Uow.RegionRepository.AllCountiesAsync()).Select(e => Mapper.Map(e));
    }
    
}