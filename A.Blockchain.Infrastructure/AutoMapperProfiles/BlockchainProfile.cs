using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.DTO.Transaction;
using AutoMapper;

namespace A.Blockchain.Infrastructure.AutoMapperProfiles
{
    public class BlockchainProfile : Profile
    {
        public BlockchainProfile()
        {
            CreateMap<BlockDTO, Block>().ReverseMap();

            CreateMap<TransactionDTO, Transaction>().ReverseMap();
        }
    }
}
