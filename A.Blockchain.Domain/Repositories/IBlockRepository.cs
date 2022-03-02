using A.Blockchain.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Domain.Repositories
{
    public interface IBlockRepository : IRepository<Block>
    {
        Block GetLatestBlock();
    }
}
