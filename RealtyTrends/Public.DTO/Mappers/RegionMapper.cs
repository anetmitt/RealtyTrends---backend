using AutoMapper;
using DAL.Base;
using Public.DTO.v1;

namespace Public.DTO.Mappers;

public class RegionMapper : BaseMapper<BLL.DTO.Region, Region>
{
    public RegionMapper(IMapper mapper) : base(mapper)
    {
    }
}