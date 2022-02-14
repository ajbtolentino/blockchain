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

        public ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> requestBlockDTO)
        {
            var block = new Block(requestBlockDTO.Data.Hash, requestBlockDTO.Data.PreviousHash)
            {
                Data = requestBlockDTO.Data.Data
            };

            var result = this.blockchainRepository.Add(block);

            return new ResponseDTO<BlockDTO>("Success", requestBlockDTO.Data);
        }

        public ResponseDTO<bool> CreateGenesisBlock()
        {
            var result = this.blockchainRepository.CreateGenesisBlock();

            return new ResponseDTO<bool>("Success", true);
        }

        public ResponseDTO<IEnumerable<BlockDTO>> GetAll()
        {
            var result = this.blockchainRepository.GetAll().Select(_ => new BlockDTO(_.Hash, _.PreviousHash)
            {

            });

            return new ResponseDTO<IEnumerable<BlockDTO>>("Success", result);
        }

        public ResponseDTO<BlockDTO> GetLatestBlock()
        {
            var blocks = this.blockchainRepository.GetLatestBlock();

            var blockDTO = new BlockDTO(blocks.Hash, blocks.PreviousHash);

            return new ResponseDTO<BlockDTO>("Success", blockDTO);
        }
    }
}
