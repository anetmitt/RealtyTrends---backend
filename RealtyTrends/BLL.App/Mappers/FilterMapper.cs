using AutoMapper;
using BLL.DTO;
using DAL.Base;

namespace BLL.App.Mappers;

public class FilterMapper : BaseMapper<Domain.App.Filter, Filter>
{
    public FilterMapper(IMapper mapper) : base(mapper)
    {
    }
}