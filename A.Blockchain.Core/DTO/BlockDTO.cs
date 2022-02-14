using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class BlockDTO : DTOBase
    {
        public BlockDTO(string hash, string previousHash)
        {
            this.Hash = hash;
            this.PreviousHash = previousHash;
        }

        public int Id { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public string Data { get; set; }
        public int Nonce { get; set; }
        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
