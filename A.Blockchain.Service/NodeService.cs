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
    public class NodeService : INodeService
    {
        private readonly IBlockRepository blockRepository;

        public NodeService(IBlockRepository blockRepository)
        {
            this.blockRepository = blockRepository;
        }

        public ResponseDTO<bool> ValidateBlock(RequestDTO<BlockDTO> block)
        {
            var latestBlock = blockRepository.GetLatestBlock();

            if(latestBlock == null) return new ResponseDTO<bool>("Latest block not found", false);
            if (latestBlock.Hash == block.Data.Hash) return new ResponseDTO<bool>("Success", true);

            return new ResponseDTO<bool>("Invalid block", false);
        }
    }
}
