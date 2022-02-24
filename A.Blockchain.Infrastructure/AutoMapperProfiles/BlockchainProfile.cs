using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
