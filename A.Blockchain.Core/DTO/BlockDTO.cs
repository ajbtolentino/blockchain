using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class BlockDTO : DTOBase
    {
        public BlockDTO()
        {

        }

        public BlockDTO(int id, string hash, string previousHash, int nonce, DateTime timestamp)
        {
            this.Id = id;
            this.Hash = hash;
            this.PreviousHash = previousHash;
            this.Nonce = nonce;
            this.Timestamp = timestamp;
        }

        public BlockDTO(string hash, DateTime timestamp, string? previousHash = null)
        {
            this.Hash = hash;
            this.Timestamp = timestamp;
            this.PreviousHash = previousHash ?? string.Empty;
        }

        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
