using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class ProofOfWorkService : IBlockchainService
    {
        private readonly IBlockRepository blockchainRepository;
        private readonly ITransactionRepository transactionRepository;

        //Store in settings
        private static int difficulty = 5;


        public ProofOfWorkService(IBlockRepository blockchainRepository, ITransactionRepository transactionRepository)
        {
            this.blockchainRepository = blockchainRepository;
            this.transactionRepository = transactionRepository;
        }

        public ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> blockRequest)
        {
            var previousBlock = this.GetLatestBlock();
            var previousHash = previousBlock.Hash;

            var newBlock = this.MineBlock(new BlockDTO(string.Empty, DateTime.Now, previousHash)
            {
                Id = previousBlock.Id + 1
            });

            blockchainRepository.Add(new Block(newBlock.Id, newBlock.Hash, newBlock.PreviousHash, newBlock.Timestamp, newBlock.Nonce));

            return new ResponseDTO<BlockDTO>("Success", newBlock);
        }

        private BlockDTO MineBlock(BlockDTO block)
        {
            var diffString = new string[difficulty];
            Array.Fill(diffString, "0", 0, difficulty);

            do
            {
                block.Hash = CalculateHash(block.Id, block.PreviousHash, block.Timestamp, block.Nonce);
                block.Nonce++;
            } while (block.Hash.Substring(0, difficulty) != String.Concat(diffString));

            return block;
        }

        private BlockDTO CreateGenesisBlock()
        {
            var genesisBlock = this.MineBlock(new BlockDTO(CalculateHash(0, string.Empty, DateTime.UtcNow, 0), DateTime.UtcNow));

            blockchainRepository.Add(new Block(0, genesisBlock.Hash, genesisBlock.PreviousHash, genesisBlock.Timestamp, genesisBlock.Nonce));

            return genesisBlock;
        }

        private BlockDTO GetLatestBlock() {
            if (!this.Any())
            {
                return this.CreateGenesisBlock();
            }

            return this.LastOrDefault();
        }

        private static string CalculateHash(int index, string previousHash, DateTime timestamp, int nonce)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                var input = $"{index}{previousHash}{timestamp}{nonce}";

                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public IEnumerator<BlockDTO> GetEnumerator() => blockchainRepository.GetAll()
                                                                            .Select(_ => new BlockDTO(_.Id, _.Hash, _.PreviousHash, _.Nonce, _.Timestamp))
                                                                            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
