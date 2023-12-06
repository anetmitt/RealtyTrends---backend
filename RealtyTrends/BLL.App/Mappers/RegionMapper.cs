using AutoMapper;
using DAL.Base;

namespace BLL.App.Mappers;

public class RegionMapper: BaseMapper<BLL.DTO.Region, Domain.App.Region>
{
    public RegionMapper(IMapper mapper) : base(mapper)
    {
    }
}