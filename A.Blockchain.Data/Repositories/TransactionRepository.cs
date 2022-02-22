using A.Blockchain.Core.Constants;
using A.Blockchain.Core.Domain;
using A.Blockchain.Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Data.Repositories
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public IEnumerable<Transaction> GetPendingTransactions()
        {
            return this.GetAll().Where(_ => _.State == TransactionState.PENDING);
        }
    }
}
