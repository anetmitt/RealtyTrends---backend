using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class SnapshotMapper: BaseMapper<BLL.DTO.Snapshot, v1.Snapshot>
{
    public SnapshotMapper(IMapper mapper) : base(mapper)
    {
    }
}