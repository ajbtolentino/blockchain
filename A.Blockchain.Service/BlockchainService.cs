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
    public class BlockchainService : IBlockchainService
    {
        private readonly IBlockRepository blockchainRepository;
        private readonly ITransactionRepository transactionRepository;

        public BlockchainService(IBlockRepository blockchainRepository, ITransactionRepository transactionRepository)
        {
            this.blockchainRepository = blockchainRepository;
            this.transactionRepository = transactionRepository;
        }

        public ResponseDTO<BlockDTO> AddBlock(RequestDTO<BlockDTO> block)
        {
            var newBlock = this.blockchainRepository.Add(new Block
            {
                Hash = block.Data.Hash,
                Height = block.Data.Height,
                Nonce = block.Data.Nonce,
                PreviousHash = block.Data.PreviousHash,
                Timestamp = block.RequestedDate,
                Transactions = block.Data.Transactions.Select(_ => new Transaction
                {
                    Amount = _.Amount,
                    From = _.From,
                    Timestamp = _.Timestamp,
                    To = _.To
                })
            });

            return new ResponseDTO<BlockDTO>("Success", this.GetBlockByHash(newBlock.Hash));
        }

        public ResponseDTO<TransactionDTO> AddTransaction(RequestDTO<TransactionDTO> transaction)
        {
            var result = this.transactionRepository.Add(new Transaction
            {
                Amount = transaction.Data.Amount,
                From = transaction.Data.From,
                Timestamp = transaction.RequestedDate,
                To = transaction.Data.To,
                State = TransactionState.PENDING
            });

            return new ResponseDTO<TransactionDTO>("Success", new TransactionDTO
            {
                Amount = result.Amount,
                From = result.From,
                To = result.To,
                Timestamp = result.Timestamp
            });
        }

        //TODO: Move to miner service
        public ResponseDTO<BlockDTO> Mine()
        {
            var pendingTransactions = this.transactionRepository.GetPendingTransactions();

            if (!pendingTransactions.Any()) return new ResponseDTO<BlockDTO>("Failed", null);

            var previousBlock = this.GetLatestBlock();

            var newBlock = new BlockDTO
            {
                Height = previousBlock.Height + 1,
                PreviousHash = previousBlock.Hash,
                Transactions = pendingTransactions.Select(_ => new TransactionDTO
                {
                    Amount = _.Amount,
                    From = _.From,
                    To = _.To,
                    Timestamp = _.Timestamp
                }),
                Timestamp = DateTime.UtcNow
            }.MineBlock(5);

            blockchainRepository.Add(new Block
            {
                Height = newBlock.Height,
                Hash = newBlock.Hash,
                PreviousHash = newBlock.PreviousHash,
                Nonce = newBlock.Nonce,
                Timestamp = newBlock.Timestamp,
                Transactions = pendingTransactions
            });

            foreach(var transaction in pendingTransactions)
            {
                transaction.BlockIndex = newBlock.Height;
                transaction.State = TransactionState.SUCCESS;
            }

            this.transactionRepository.UpdateRange(pendingTransactions);
            
            return new ResponseDTO<BlockDTO>("Success", this.GetBlockByHash(newBlock.Hash));
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
                        Timestamp = timestamp
                    }
                }
            };

            var minedGenesisBlock = genesisBlock.MineBlock(5);

            blockchainRepository.Add(new Block
            {
                Height = minedGenesisBlock.Height,
                Hash = minedGenesisBlock.Hash,
                PreviousHash = minedGenesisBlock.PreviousHash,
                Nonce = minedGenesisBlock.Nonce,
                Timestamp = minedGenesisBlock.Timestamp
            });

            transactionRepository.AddRange(minedGenesisBlock.Transactions.Select(_ => new Transaction
            {
                Amount = _.Amount,
                From = _.From,
                State = TransactionState.SUCCESS,
                Timestamp = _.Timestamp,
                To = _.To
            }));

            return this.FirstOrDefault() ?? throw new Exception("Genesis block not found");
        }

        public IEnumerator<BlockDTO> GetEnumerator()
        {
            foreach (var block in blockchainRepository.GetAll())
            {
                var transactions = this.transactionRepository.GetAll().Where(_ => _.BlockIndex == block.Height);

                yield return new BlockDTO
                {
                    Height = block.Height,
                    Hash = block.Hash,
                    PreviousHash = block.PreviousHash,
                    Nonce = block.Nonce,
                    Timestamp = block.Timestamp,
                    Transactions = transactions.Select(_ => new TransactionDTO
                    {
                        Amount = _.Amount,
                        From = _.From,
                        Timestamp = _.Timestamp,
                        To = _.To
                    })
                };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private BlockDTO GetLatestBlock()
        {
            if (!this.Any())
            {
                return this.CreateGenesisBlock();
            }

            return this.LastOrDefault() ?? throw new Exception("Latest block not found");
        }

        private BlockDTO GetBlockByHash(string hash)
        {
            var block = this.FirstOrDefault(_ => _.Hash == hash);
            var transactions = this.transactionRepository.GetAll().Where(_ => _.BlockIndex == block.Height);

            return new BlockDTO
            {
                Hash = block.Hash,
                Height = block.Height,
                Nonce = block.Nonce,
                PreviousHash = block.PreviousHash,
                Timestamp = block.Timestamp,
                Transactions = transactions.Select(_ =>
                new TransactionDTO
                {
                    Amount = _.Amount,
                    Timestamp = _.Timestamp,
                    From = _.From,
                    To = _.To
                })
            };
        }
    }
}
