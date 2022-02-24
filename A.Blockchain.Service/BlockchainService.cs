using A.Blockchain.Core.Constants;
using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;

namespace A.Blockchain.Service
{
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> pendingTransactionRepository;
        
        private readonly IHashService hashService;
        private readonly INodeService nodeService;

        public BlockchainService(IBlockRepository blockchainRepository, 
                                 IRepository<Transaction> pendingTransactionRepository,
                                 IHashService hashService,
                                 INodeService nodeService)
        {
            this.blockRepository = blockchainRepository;
            this.pendingTransactionRepository = pendingTransactionRepository;

            this.hashService = hashService;
            this.nodeService = nodeService;
        }

        public ResponseDTO<bool> Initialize()
        {
            if (!this.blockRepository.GetAll().Any(_ => _.Height == 0)) this.CreateGenesisBlock();

            return new ResponseDTO<bool>("Success", true);
        }

        private BlockDTO CreateGenesisBlock()
        {
            var block = new BlockDTO
            {
                PreviousHash = string.Empty,
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

            this.blockRepository.Add(new Block
            {
                Hash = block.Hash,
                Nonce = block.Nonce,
                Timestamp = block.Timestamp
            });

            return block;
        }

        public ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> block)
        {
            var validationResult = this.nodeService.ValidateBlock(block);

            if (!validationResult.Data) return new ResponseDTO<BlockDTO>(validationResult.Message, null);

            var newBlock = this.blockRepository.Add(new Block
            {
                Hash = block.Data.Hash,
                Height = block.Data.Height,
                Nonce = block.Data.Nonce,
                PreviousHash = block.Data.PreviousHash,
                Timestamp = block.RequestedDate,
                Transactions = block.Data.Transactions.Select(_ => new Transaction
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                }).ToList()
            });

            foreach(var transaction in block.Data.Transactions)
            {
                this.pendingTransactionRepository.Delete(new Transaction
                {
                    Id = transaction.Id
                });
            }

            return new ResponseDTO<BlockDTO>("Success", new BlockDTO
            {
                Hash = newBlock.Hash,
                Height = newBlock.Height,
                Nonce = newBlock.Nonce,
                PreviousHash = newBlock.PreviousHash,
                Timestamp = newBlock.Timestamp,
                Transactions = newBlock.Transactions.Select(_ => new TransactionDTO
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                }).ToList()
            });
        }

        public ResponseDTO<IEnumerable<BlockDTO>> GetAllBlocks()
        {
            var blocks = blockRepository.GetAll().Select(_ => new BlockDTO
            {
                Height = _.Height,
                Hash = _.Hash,
                PreviousHash = _.PreviousHash,
                Nonce = _.Nonce,
                Timestamp = _.Timestamp,
                Transactions = _.Transactions.Select(_ => new TransactionDTO
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });

            return new ResponseDTO<IEnumerable<BlockDTO>>("Success", blocks);
        }

        public ResponseDTO<BlockDTO> GetLatestBlock()
        {
            var blocks = blockRepository.GetAll();

            var latestBlock = blocks.LastOrDefault();

            if (latestBlock == null) return new ResponseDTO<BlockDTO>("Latest block not found", null);

            return new ResponseDTO<BlockDTO>("Success",  new BlockDTO
            {
                Hash = latestBlock.Hash,
                Height = latestBlock.Height,
                Nonce = latestBlock.Nonce,
                PreviousHash= latestBlock.PreviousHash,
                Timestamp = latestBlock.Timestamp,
                Transactions = latestBlock.Transactions.Select(_ => new TransactionDTO
                {
                    Id = _.Id,
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });
        }

        public ResponseDTO<IEnumerable<TransactionDTO>> GetPendingTransactions()
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll()
                                                                .Select(_ => new TransactionDTO
                                                                {
                                                                    Id = _.Id,
                                                                    Amount = _.Amount,
                                                                    From = _.From,
                                                                    Timestamp = _.Timestamp,
                                                                    To = _.To
                                                                });

            return new ResponseDTO<IEnumerable<TransactionDTO>>("Success", pendingTransactions);
        }
    }
}
