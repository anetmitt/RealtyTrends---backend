using AutoMapper;
using Public.DTO.v1;

namespace Public.DTO;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<BLL.DTO.Filter, Filter>().ReverseMap();
        CreateMap<BLL.DTO.FilterType, FilterType>().ReverseMap();
        CreateMap<BLL.DTO.Trigger, v1.Trigger>();
        CreateMap<BLL.DTO.WriteTrigger, WriteTrigger>();
        CreateMap<BLL.DTO.TriggerFilter, TriggerFilter>();
        CreateMap<BLL.DTO.StatisticFilters, v1.StatisticFilters>().ReverseMap();
        CreateMap<BLL.DTO.Snapshot, v1.Snapshot>().ReverseMap();
        CreateMap<BLL.DTO.Region, Region>().ReverseMap();
        CreateMap<BLL.DTO.PropertyType, PropertyType>().ReverseMap();
        CreateMap<BLL.DTO.TransactionType, TransactionType>().ReverseMap();
    }
}