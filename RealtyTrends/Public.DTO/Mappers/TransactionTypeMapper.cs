using AutoMapper;
using DAL.Base;

namespace Public.DTO.Mappers;

public class TransactionTypeMapper : BaseMapper<BLL.DTO.TransactionType, v1.TransactionType>
{
    public TransactionTypeMapper(IMapper mapper) : base(mapper)
    {
    }
}