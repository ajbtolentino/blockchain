using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.DTO;
using A.Blockchain.Domain.Entities;
using A.Blockchain.Domain.Repositories;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace A.Blockchain.Application.Handlers
{
    internal sealed class BlockCommandHandlers : ICommandHandler<AddBlockCommand>, 
                                                 ICommandResultHandler<MineBlockCommand, BlockDTO>
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> transactionRepository;

        public BlockCommandHandlers(IBlockRepository blockRepository, 
                                    IRepository<Transaction> transactionRepository)
        {
            this.blockRepository = blockRepository;
            this.transactionRepository = transactionRepository;
        }

        public Task HandleAsync(AddBlockCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<BlockDTO> HandleAsync(MineBlockCommand command)
        {
            var latestBlock = this.blockRepository.GetLatestBlock();
            var transactions = this.transactionRepository.GetAll()
                                                                .Where(_ => command.transactions.Contains(_.Id));

            var currentHeight = latestBlock.Height + 1;
            var currentHash = string.Empty;
            var currentNonce = 0;
            var difficulty = command.difficulty;
            var diffString = new string[difficulty];

            Array.Fill(diffString, "0", 0, difficulty);

            do
            {
                using (var sha = SHA256.Create())
                {
                    var result = sha.ComputeHash(Encoding.UTF8.GetBytes($"{currentHeight + 1}{latestBlock.Hash}{currentNonce}"));

                    currentHash = Convert.ToHexString(result).ToLower();
                }

                currentNonce++;
            } while (currentHash[..difficulty] != String.Concat(diffString));

            return new BlockDTO
            {
                Hash = currentHash,
                Height = currentHeight,
                Nonce = currentNonce,
                PreviousHash = latestBlock.Hash,
                Timestamp = DateTime.UtcNow,
                Transactions = transactions.Select(_ => new TransactionDTO
                {
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                }),
            };
        }
    }
}
