using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Queries
{
    public record GetBalanceQuery(string address) : IQuery<decimal>;
    public record GetPendingTransactionsQuery() : IQuery<IEnumerable<TransactionDTO>>;
}
