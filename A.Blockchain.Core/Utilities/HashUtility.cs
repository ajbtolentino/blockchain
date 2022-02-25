using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using A.Blockchain.Core.Interfaces.Service;

namespace A.Blockchain.Core.Utilities
{
    public static class HashUtility
    {
        public static string CalculateHash<T>(T data)
        {
            using (var hash = SHA256.Create())
            {
                var result = hash.ComputeHash(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data)));

                return Convert.ToHexString(result).ToLower();
            }
        }
    }
}
