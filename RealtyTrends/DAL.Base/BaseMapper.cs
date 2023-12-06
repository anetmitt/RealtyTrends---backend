using AutoMapper;
using Contracts.Base;

namespace DAL.Base;

public class BaseMapper<TSource, TDestination>: IMapper<TSource, TDestination>
{
    protected readonly IMapper _mapper;

    public BaseMapper(IMapper mapper)
    {
        _mapper = mapper;
    }

    public virtual TSource? Map(TDestination? entity)
    {
        return _mapper.Map<TSource>(entity);
    }

    public virtual TDestination? Map(TSource? entity)
    {
        return _mapper.Map<TDestination>(entity);
    }
}