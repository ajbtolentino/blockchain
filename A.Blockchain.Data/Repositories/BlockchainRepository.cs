using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.Repositories
{
    public class BlockchainRepository : RepositoryBase<Block>, IBlockchainRepository
    {
        public void Create(Block block)
        {
            throw new NotImplementedException();
        }
    }
}
