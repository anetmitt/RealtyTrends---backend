using BLL.Base;
using BLL.Contracts.App;
using Contracts.Base;
using DAL.Contracts.App;
using Domain.App;

namespace BLL.App.Services;

public class TriggerFiltersService: BaseEntityService<BLL.DTO.TriggerFilter, TriggerFilter, ITriggerFilterRepository>, ITriggerFiltersService
{
    protected readonly IAppUow Uow;
    
    public TriggerFiltersService(IMapper<DTO.TriggerFilter, TriggerFilter> mapper, IAppUow uow) : base(uow.TriggerFilterRepository, mapper)
    {
        Uow = uow;
    }


    public async Task<IEnumerable<DTO.TriggerFilter>> GetAllTriggerFiltersAsync(Guid triggerId)
    {
        return (await Uow.TriggerFilterRepository.GetAllTriggerFiltersAsync(triggerId)).Select(e => Mapper.Map(e));
    }
}