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
        public string Calculate<T>(T data)
        {
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));

                return Convert.ToHexString(result).ToLower();
            }
        }
    }
}
