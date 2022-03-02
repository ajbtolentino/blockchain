using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.DTO;
using A.Blockchain.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Handlers
{
    internal sealed class BlockQueryHandlers : IQueryHandler<GetAllBlocksQuery, IEnumerable<BlockDTO>>
    {
        public Task<IEnumerable<BlockDTO>> HandleAsync(GetAllBlocksQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
