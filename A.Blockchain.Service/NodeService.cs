using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.DTO.Transaction;
using A.Blockchain.Core.Interfaces;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class NodeService : ServiceBase, INodeService
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> transactionRepository;

        public NodeService(IBlockRepository blockRepository,
                        IRepository<Transaction> transactionRepository,
                        IObjectMapper mapper) : base(mapper)
        {
            this.blockRepository = blockRepository;
            this.transactionRepository = transactionRepository;
        }

        public ResponseDTO<BlockDTO> AddBlock(BlockDTO block)
        {
            var newBlock = this.blockRepository.Add(this.Map<Block>(block));

            return new ResponseDTO<BlockDTO>("Success", this.Map<BlockDTO>(newBlock));
        }

        public ResponseDTO<bool> DeleteTransactions(params int[] transactionIds)
        {
            var result = this.transactionRepository.DeleteAll(transactionIds);

            return new ResponseDTO<bool>("Success", result);
        }

        public ResponseDTO<IEnumerable<BlockDTO>> GetAllBlocks()
        {
            var blocks = blockRepository.GetAll().Select(_ => this.Map<BlockDTO>(_));

            return new ResponseDTO<IEnumerable<BlockDTO>>("Success", blocks);
        }

        public ResponseDTO<BlockDTO> GetLatestBlock()
        {
            var latestBlock = blockRepository.GetLatestBlock();

            var result = latestBlock switch
            {
                null => new ResponseDTO<BlockDTO>("Latest block not found", null),
                _ => new ResponseDTO<BlockDTO>("Success", this.Map<BlockDTO>(latestBlock)),
            };

            return result;
        }

        public ResponseDTO<IEnumerable<TransactionDTO>> GetPendingTransactions()
        {
            var pendingTransactions = this.transactionRepository.GetAll()
                                                                .Select(_ => this.Map<TransactionDTO>(_));

            return new ResponseDTO<IEnumerable<TransactionDTO>>("Success", pendingTransactions);
        }
    }
}
