using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
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
        private readonly IBlockRepository blockchainRepository;

        public BlockchainService(IBlockRepository blockchainRepository)
        {
            this.blockchainRepository = blockchainRepository;
        }

        public BlockDTO AddBlock(BlockDTO block)
        {
            throw new NotImplementedException();
        }

        public BlockDTO CreateGenesisBlock()
        {
            var result = this.blockchainRepository.Add(new Block("",""));

            return new BlockDTO("Test", "Test");
        }

        public BlockDTO GetLatestBlock()
        {
            var blocks = this.blockchainRepository.GetAll().ElementAt(0);

            return new BlockDTO(blocks.Hash, blocks.PreviousHash);
        }
    }
}
