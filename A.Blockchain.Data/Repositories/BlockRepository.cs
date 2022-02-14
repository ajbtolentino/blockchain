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
        public Block CreateGenesisBlock()
        {
            var result = new Block("", "");
            result.Id = 1;

            base.Add(result);

            return result;
        }

        public Block GetLatestBlock()
        {
            return base.GetAll().ElementAt(0);
        }
    }
}
