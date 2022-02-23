using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.Repositories
{
    public class BlockRepository : RepositoryBase<Block>, IBlockRepository
    {
        public Block GetLatestBlock()
        {
            if (!base.GetAll().Any()) return null;

            return base.GetAll().ElementAt(0);
        }
    }
}
