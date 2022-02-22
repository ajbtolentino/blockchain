using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A.Blockchain.Core.DTO
{
    public class BlockDTO : DTOBase
    {
        public int Height { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public DateTime Timestamp { get; set; }

        public IEnumerable<TransactionDTO> Transactions { get; set; }

        public BlockDTO MineBlock(int difficulty)
        {
            var diffString = new string[difficulty];
            Array.Fill(diffString, "0", 0, difficulty);

            do
            {
                this.Hash = this.CalculateHash();
                this.Nonce++;
            } while (this.Hash[..difficulty] != String.Concat(diffString));

            return this;
        }

        private string CalculateHash()
        {
            var builder = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this)));

                foreach (byte b in result)
                {
                    builder.Append(b.ToString("x2"));
                }
            }

            return builder.ToString();
        }
    }
}
