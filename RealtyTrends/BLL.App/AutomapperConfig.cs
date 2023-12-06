using AutoMapper;
using Domain.App;

namespace BLL.App;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Filter, BLL.DTO.Filter>().ReverseMap();
        CreateMap<FilterType, BLL.DTO.FilterType>().ReverseMap();
        CreateMap<Trigger, BLL.DTO.Trigger>();
        CreateMap<UserTrigger, BLL.DTO.UserTrigger>().ReverseMap();
        CreateMap<TriggerFilter, BLL.DTO.TriggerFilter>().ReverseMap();
        CreateMap<Region, BLL.DTO.Region>().ReverseMap();
        CreateMap<RegionType, BLL.DTO.RegionType>().ReverseMap();
        CreateMap<PropertyType, BLL.DTO.PropertyType>().ReverseMap();
        CreateMap<Snapshot, BLL.DTO.Snapshot>().ReverseMap();
        CreateMap<TransactionType, BLL.DTO.TransactionType>().ReverseMap();
    }
}