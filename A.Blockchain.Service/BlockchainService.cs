using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockchainRepository blockchainRepository;

        public BlockchainService(IBlockchainRepository blockchainRepository)
        {
            this.blockchainRepository = blockchainRepository;
        }

        public void CreateGenesisBlock()
        {
            blockchainRepository.Create(new Block());
        }
    }
}
