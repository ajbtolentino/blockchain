using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class TransactionDTO : DTOBase
    {
        public int BlockId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
