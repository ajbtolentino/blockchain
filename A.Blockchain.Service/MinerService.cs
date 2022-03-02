using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.Interfaces.Service;
using A.Blockchain.Core.Utilities;

namespace A.Blockchain.Service
{
    public class MinerService : IMinerService
    {
        public void Mine(BlockDTO block)
        {
            var difficulty = 3;
            var diffString = new string[difficulty];

            Array.Fill(diffString, "0", 0, difficulty);
            string? hash;

            do
            {
                hash = HashUtility.CalculateHash(block);

                block.Nonce++;
            } while (hash[..difficulty] != String.Concat(diffString));

            block.Hash = hash.ToLower();
        }
    }
}
