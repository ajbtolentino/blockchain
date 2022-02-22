using A.Blockchain.Core.Constants;
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
using System.Text.Json;
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
            var pendingTransactions = this.transactionRepository.GetPendingTransactions();

            var newBlock = this.MineBlock(new BlockDTO
            {
                Index = previousBlock.Index + 1,
                PreviousHash = previousBlock.Hash,
                Transactions = pendingTransactions.Select(_ => new TransactionDTO
                {
                    Amount = _.Amount,
                    From = _.From,
                    To = _.To,
                    Timestamp = _.Timestamp,
                    State = _.State
                }),
                Timestamp = DateTime.UtcNow
            });

            blockchainRepository.Add(new Block
            {
                Index = newBlock.Index,
                Hash = newBlock.Hash,
                PreviousHash = newBlock.PreviousHash,
                Nonce = newBlock.Nonce,
                Timestamp= newBlock.Timestamp,
                Transactions = pendingTransactions
            });

            //Update transaction state

            return new ResponseDTO<BlockDTO>("Success", newBlock);
        }

        private BlockDTO MineBlock(BlockDTO block)
        {
            var diffString = new string[difficulty];
            Array.Fill(diffString, "0", 0, difficulty);

            do
            {
                var input = $"{JsonSerializer.Serialize(block)}";

                block.Hash = CalculateHash(input);
                block.Nonce++;
            } while (block.Hash[..difficulty] != String.Concat(diffString));

            return block;
        }

        private BlockDTO CreateGenesisBlock()
        {
            var timestamp = DateTime.UtcNow;

            var genesisBlock = new BlockDTO
            {
                Timestamp = timestamp,
                Transactions = new List<TransactionDTO>() {
                    new TransactionDTO
                    {
                        Amount = 1000,
                        To = "Allan",
                        From = "Allan",
                        Timestamp = timestamp,
                        State = TransactionState.SUCCESS
                    } 
                }
            };

            var minedGenesisBlock = this.MineBlock(genesisBlock);

            blockchainRepository.Add(new Block
            {
                Index = minedGenesisBlock.Index,
                Hash = minedGenesisBlock.Hash,
                PreviousHash = minedGenesisBlock.PreviousHash,
                Nonce = minedGenesisBlock.Nonce,
                Timestamp = minedGenesisBlock.Timestamp,
                Transactions = minedGenesisBlock.Transactions.Select(_ => new Transaction
                {
                    Amount= _.Amount,
                    From = _.From,
                    State = _.State,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });

            return this.FirstOrDefault() ?? throw new Exception("Genesis block not found");
        }

        private BlockDTO GetLatestBlock() {
            if (!this.Any())
            {
                return this.CreateGenesisBlock();
            }

            return this.LastOrDefault() ?? throw new Exception("Latest block not found");
        }

        //TODO: Better implementation i.e. HashService
        private static string CalculateHash(string input)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
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

        public IEnumerator<BlockDTO> GetEnumerator()
        {
            foreach(var block in blockchainRepository.GetAll())
            {
                yield return new BlockDTO
                {
                    Index = block.Index,
                    Hash = block.Hash,
                    PreviousHash = block.PreviousHash,
                    Nonce = block.Nonce,
                    Timestamp = block.Timestamp,
                    Transactions = block.Transactions.Select(_ => new TransactionDTO
                    {
                        Amount = _.Amount,
                        From = _.From,
                        Timestamp = _.Timestamp,
                        To = _.To,
                        State = _.State
                    })
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
