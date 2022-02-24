using A.Blockchain.Core.Constants;
using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;

namespace A.Blockchain.Service
{
    public class BlockchainService : ServiceBase, IBlockchainService
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> pendingTransactionRepository;

        private readonly IMinerService minerService;
        private readonly IHashService hashService;
        private readonly INodeService nodeService;

        public BlockchainService(IBlockRepository blockchainRepository, 
                                 IRepository<Transaction> pendingTransactionRepository,
                                 IMinerService minerService,
                                 IHashService hashService,
                                 INodeService nodeService,
                                 IObjectMapper mapper) : base(mapper)
        {
            this.blockRepository = blockchainRepository;
            this.pendingTransactionRepository = pendingTransactionRepository;

            this.minerService = minerService;
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
            var block = this.minerService.Mine(0);

            var genesisBlock = this.blockRepository.Add(this.Map<Block>(block.Data));

            return this.Map<BlockDTO>(genesisBlock);
        }

        public ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> block)
        {
            var validationResult = this.nodeService.ValidateBlock(block);

            if (!validationResult.Data) return new ResponseDTO<BlockDTO>(validationResult.Message, null);

            var newBlock = this.blockRepository.Add(this.Map<Block>(block.Data));

            foreach(var transaction in block.Data.Transactions)
            {
                this.pendingTransactionRepository.Delete(new Transaction
                {
                    Id = transaction.Id
                });
            }

            return new ResponseDTO<BlockDTO>("Success", this.Map<BlockDTO>(newBlock));
        }

        public ResponseDTO<IEnumerable<BlockDTO>> GetAllBlocks()
        {
            var blocks = blockRepository.GetAll().Select(_ => this.Map<BlockDTO>(_));

            return new ResponseDTO<IEnumerable<BlockDTO>>("Success", blocks);
        }

        public ResponseDTO<BlockDTO> GetLatestBlock()
        {
            var blocks = blockRepository.GetAll();

            var latestBlock = blocks.LastOrDefault();

            if (latestBlock == null) return new ResponseDTO<BlockDTO>("Latest block not found", null);

            return new ResponseDTO<BlockDTO>("Success", this.Map<BlockDTO>(latestBlock));
        }

        public ResponseDTO<IEnumerable<TransactionDTO>> GetPendingTransactions()
        {
            var pendingTransactions = this.pendingTransactionRepository.GetAll()
                                                                .Select(_ => this.Map<TransactionDTO>(_));

            return new ResponseDTO<IEnumerable<TransactionDTO>>("Success", pendingTransactions);
        }
    }
}
