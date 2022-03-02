using A.Blockchain.Application.Common.Interfaces;
using A.Blockchain.Domain.Entities;
using A.Blockchain.Domain.Repositories;

namespace A.Blockchain.Infrastructure.Repositories
{
    public class BlockRepository : GenericRepository<Block>, IBlockRepository
    {
        public BlockRepository(IBlockchainDbContext dbContext)
            : base(dbContext) { }

        public Block GetLatestBlock()
        {
            if (!base.GetAll().Any()) return null;

            return base.GetAll().LastOrDefault();
        }
    }
}
