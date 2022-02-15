using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Domain
{
    public class Block
    {
        public Block(string hash, string previousHash, DateTime timestamp, IEnumerable<Transaction>? transactions = null)
        {
            this.Hash = hash;
            this.PreviousHash = previousHash;
            this.Transactions = transactions ?? new List<Transaction>();
        }

        public int Id { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
