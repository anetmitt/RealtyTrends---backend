using AutoMapper;
using DAL.Base;
using Domain.App;

namespace BLL.App.Mappers;

public class TransactionTypeMapper : BaseMapper<BLL.DTO.TransactionType, TransactionType>
{
    public TransactionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}