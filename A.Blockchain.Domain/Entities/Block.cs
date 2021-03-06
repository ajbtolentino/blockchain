using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Domain.Entities
{
    public class Block : EntityBase
    {
        public int Height { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public DateTime Timestamp { get; set; }
    }
}
