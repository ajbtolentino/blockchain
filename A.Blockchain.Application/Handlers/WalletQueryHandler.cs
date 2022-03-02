using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Handlers
{
    internal class WalletQueryHandler : IQueryHandler<GetBalanceQuery, decimal>
    {
        public Task<decimal> HandleAsync(GetBalanceQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
