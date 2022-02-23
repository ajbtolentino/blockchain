using A.Blockchain.Core.Constants;
using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class MinerService : IMinerService
    {
        private readonly IBlockRepository blockRepository;
        private readonly ITransactionRepository transactionRepository;
        private readonly IHashService hashService;

        public MinerService(IBlockRepository blockRepository, 
                            ITransactionRepository transactionRepository,
                            IHashService hashService)
        {
            this.blockRepository = blockRepository;
            this.transactionRepository = transactionRepository;
            this.hashService = hashService;
        }

        public ResponseDTO<BlockDTO> Mine()
        {
            var pendingTransactions = this.transactionRepository.GetPendingTransactions();

            if (!pendingTransactions.Any()) return new ResponseDTO<BlockDTO>("No pending transactions", null);

            var latestBlock = this.blockRepository.GetLatestBlock();

            if(latestBlock == null) return new ResponseDTO<BlockDTO>("Missing genesis block", null);

            var block = new BlockDTO
            {
                Height = latestBlock.Height + 1,
                PreviousHash = latestBlock.Hash,
                Transactions = pendingTransactions.Select(_ => new TransactionDTO
                {
                    Amount = _.Amount,
                    From = _.From,
                    To = _.To,
                    Timestamp = _.Timestamp
                }),
                Timestamp = DateTime.UtcNow
            };

            block = this.hashService.CalculateHash(block);

            var newBlock = this.blockRepository.Add(new Block
            {
                Hash = block.Hash,
                Height = block.Height,
                Nonce = block.Nonce,
                PreviousHash= block.PreviousHash,
                Timestamp = block.Timestamp,
                Transactions = block.Transactions.Select(_ => new Transaction
                {
                    Amount= _.Amount,
                    From= _.From,
                    Timestamp= _.Timestamp,
                    To = _.To
                })
            });

            return new ResponseDTO<BlockDTO>("Success", new BlockDTO
            {
                Hash = newBlock.Hash,
                Height = newBlock.Height,
                Nonce= newBlock.Nonce,
                PreviousHash = newBlock.PreviousHash,
                Timestamp = block.Timestamp,
                Transactions = block.Transactions.Select(_ => new TransactionDTO
                {
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });
        }
    }
}
