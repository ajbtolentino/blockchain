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
        private readonly IRepository<Transaction> pendingTransactionRepository;
        private readonly IHashService hashService;

        public MinerService(IBlockRepository blockRepository,
                            IRepository<Transaction> transactionRepository,
                            IHashService hashService)
        {
            this.blockRepository = blockRepository;
            this.pendingTransactionRepository = transactionRepository;
            this.hashService = hashService;
        }

        public ResponseDTO<BlockDTO> Mine()
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll().Take(2);

            if (!pendingTransactions.Any()) return new ResponseDTO<BlockDTO>("No pending transactions", null);

            var latestBlock = this.blockRepository.GetLatestBlock();

            if(latestBlock == null) return new ResponseDTO<BlockDTO>("Missing genesis block", null);

            var block = new BlockDTO
            {
                Height = latestBlock.Height + 1,
                PreviousHash = latestBlock.Hash,
                Transactions = pendingTransactions.Select(_ => new TransactionDTO
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    To = _.To,
                    Timestamp = _.Timestamp
                }),
                Timestamp = DateTime.UtcNow
            };

            block = this.hashService.CalculateHash(block);

            return new ResponseDTO<BlockDTO>("Success", new BlockDTO
            {
                Hash = block.Hash,
                Height = block.Height,
                Nonce= block.Nonce,
                PreviousHash = block.PreviousHash,
                Timestamp = block.Timestamp,
                Transactions = block.Transactions.Select(_ => new TransactionDTO
                {
                    Id= _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });
        }
    }
}
