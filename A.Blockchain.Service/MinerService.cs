using A.Blockchain.Core.Constants;
using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces;
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
    public class MinerService : ServiceBase, IMinerService
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> pendingTransactionRepository;
        private readonly IHashService hashService;

        public MinerService(IBlockRepository blockRepository,
                            IRepository<Transaction> transactionRepository,
                            IHashService hashService,
                            IObjectMapper mapper) : base(mapper)
        {
            this.blockRepository = blockRepository;
            this.pendingTransactionRepository = transactionRepository;
            this.hashService = hashService;
        }

        public ResponseDTO<BlockDTO> Mine()
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll()
                                          .Select(_ => this.Map<TransactionDTO>(_));

            return this.Mine(pendingTransactions);
        }


        public ResponseDTO<BlockDTO> Mine(int[] pendingTransactionIds)
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll()
                                          .Where(_ => pendingTransactionIds.Contains(_.Id))
                                          .Select(_ => this.Map<TransactionDTO>(_));

            return this.Mine(pendingTransactions);
        }

        public ResponseDTO<BlockDTO> Mine(int count)
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll().Take(count)
                                          .Select(_ => this.Map<TransactionDTO>(_));

            return this.Mine(pendingTransactions);
        }

        public ResponseDTO<BlockDTO> Mine(IEnumerable<TransactionDTO> transactions)
        {
            var latestBlock = this.blockRepository.GetLatestBlock();

            var block = new BlockDTO
            {
                Height = latestBlock?.Height + 1 ?? 0,
                PreviousHash = latestBlock?.Hash ?? string.Empty,
                Transactions = transactions.Select(_ => this.Map<TransactionDTO>(_)),
                Timestamp = DateTime.UtcNow
            };

            var difficulty = 3;
            var diffString = new string[difficulty];

            Array.Fill(diffString, "0", 0, difficulty);
            var hash = string.Empty;

            do
            {
                hash = this.hashService.Calculate(block);

                block.Nonce++;
            } while (hash[..difficulty] != String.Concat(diffString));

            block.Hash = hash.ToLower();

            return new ResponseDTO<BlockDTO>("Success", this.Map<BlockDTO>(block));
        }
    }
}
