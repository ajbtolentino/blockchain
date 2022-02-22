using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Domain
{
    public class Block
    {
        public int Index { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
