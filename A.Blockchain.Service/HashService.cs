using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class HashService : IHashService
    {
        public BlockDTO CalculateHash(BlockDTO block)
        {
            var difficulty = 3;
            var diffString = new string[difficulty];

            Array.Fill(diffString, "0", 0, difficulty);

            using (var hash = SHA256.Create())
            {
                var val = string.Empty;

                do
                {
                    var result = hash.ComputeHash(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(block)));

                    val = Convert.ToHexString(result);

                    block.Nonce++;
                } while (val[..difficulty] != String.Concat(diffString));

                block.Hash = val.ToLower();
            }

            return block;
        }


    }
}
