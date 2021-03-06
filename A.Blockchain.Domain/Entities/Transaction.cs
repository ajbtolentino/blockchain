using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Domain.Entities
{
    public class Transaction : EntityBase
    {
        public string From { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string Signature { get; set; } = string.Empty;
    }
}
