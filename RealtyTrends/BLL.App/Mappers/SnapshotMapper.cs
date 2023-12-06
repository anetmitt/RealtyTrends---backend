using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class SnapshotMapper: BaseMapper<BLL.DTO.Snapshot, Domain.App.Snapshot>
{
    public SnapshotMapper(IMapper mapper) : base(mapper)
    {
    }
}