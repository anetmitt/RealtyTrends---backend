using DAL.Contracts.App;
using DAL.Contracts.Base;
using Public.DTO.v1;

namespace BLL.Contracts.App;

public interface IUserTriggerService : IBaseRepository<BLL.DTO.UserTrigger>,
    IUserTriggerRepositoryCustom<BLL.DTO.UserTrigger>
{
    public Task AddNewTriggerToUser(Guid userId, StatisticFilters triggerFilters,
        float beginningSquareMeterPrice);
}
