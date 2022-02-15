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
        private readonly ITransactionRepository transactionRepository;

        public BlockchainService(IBlockRepository blockchainRepository, ITransactionRepository transactionRepository)
        {
            this.blockchainRepository = blockchainRepository;
            this.transactionRepository = transactionRepository;
        }

        public ResponseDTO<TransactionDTO> AddTransaction(RequestDTO<TransactionDTO> block)
        {
            var result = this.transactionRepository.Add(new Transaction());

            return new ResponseDTO<TransactionDTO>("Success", new TransactionDTO());
        }

        public ResponseDTO<IEnumerable<BlockDTO>> GetAll()
        {
            var result = this.blockchainRepository.GetAll().Select(_ => 
                                new BlockDTO(_.Hash, _.PreviousHash, _.Timestamp, _.Transactions.Select(__ => new TransactionDTO ())));

            return new ResponseDTO<IEnumerable<BlockDTO>>("Success", result);
        }

        public ResponseDTO<BlockDTO> GetLatestBlock()
        {
            var result = this.blockchainRepository.GetLatestBlock();

            return new ResponseDTO<BlockDTO>("Success", new BlockDTO(result.Hash, result.PreviousHash, result.Timestamp));
        }

        public ResponseDTO<BlockDTO> Mine(RequestDTO<BlockDTO> block)
        {
            throw new NotImplementedException();
        }
    }
}
