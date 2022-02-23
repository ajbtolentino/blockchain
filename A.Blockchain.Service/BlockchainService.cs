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
        private readonly ITransactionRepository transactionRepository;
        
        private readonly IHashService hashService;
        private readonly INodeService nodeService;

        public BlockchainService(IBlockRepository blockchainRepository, 
                                 ITransactionRepository transactionRepository,
                                 IHashService hashService,
                                 INodeService nodeService)
        {
            this.blockRepository = blockchainRepository;
            this.transactionRepository = transactionRepository;

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

            block = this.hashService.CalculateHash(block);

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
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });

            return new ResponseDTO<BlockDTO>("Success", this.GetBlockByHash(newBlock.Hash));
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
                Timestamp = latestBlock.Timestamp
            });
        }

        public ResponseDTO<IEnumerable<TransactionDTO>> GetPendingTransactions()
        {
            var pendingTransactions = this.transactionRepository.GetPendingTransactions()
                                                                .Select(_ => new TransactionDTO
                                                                {
                                                                    Amount = _.Amount,
                                                                    From = _.From,
                                                                    Timestamp = _.Timestamp,
                                                                    To = _.To
                                                                });

            return new ResponseDTO<IEnumerable<TransactionDTO>>("Success", pendingTransactions);
        }

        private BlockDTO GetBlockByHash(string hash)
        {
            var block = this.blockRepository.GetAll().FirstOrDefault(_ => _.Hash == hash);
            var transactions = this.transactionRepository.GetAll().Where(_ => _.Height == block.Height);

            return new BlockDTO
            {
                Hash = block.Hash,
                Height = block.Height,
                Nonce = block.Nonce,
                PreviousHash = block.PreviousHash,
                Timestamp = block.Timestamp,
                Transactions = transactions.Select(_ =>
                new TransactionDTO
                {
                    Amount = _.Amount,
                    Timestamp = _.Timestamp,
                    From = _.From,
                    To = _.To
                })
            };
        }
    }
}
