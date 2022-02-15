using A.Blockchain.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.Repository
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block GetLatestBlock();
    }
}
