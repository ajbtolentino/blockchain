using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class WalletService : IWalletService
    {
        private readonly ITransactionRepository transactionRepository;

        public WalletService(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public ResponseDTO<TransactionDTO> Send(string fromAddress, string toAddress, decimal amount)
        {
            var transaction = this.transactionRepository.Add(new Transaction
            {
                Amount = amount,
                From = fromAddress,
                To = toAddress,
                Timestamp = DateTime.UtcNow
            });

            return new ResponseDTO<TransactionDTO>("Success", new TransactionDTO
            {
                Amount = transaction.Amount,
                Timestamp = transaction.Timestamp,
                From = transaction.From,
                To = transaction.To
            });
        }

        public ResponseDTO<TransactionDTO> Fund(string toAddress, decimal amount)
        {
            var transaction = this.transactionRepository.Add(new Transaction
            {
                Amount = amount,
                From = "System",
                To = toAddress,
                Timestamp = DateTime.UtcNow
            });

            return new ResponseDTO<TransactionDTO>("Success", new TransactionDTO
            {
                Amount = transaction.Amount,
                Timestamp = transaction.Timestamp,
                From = transaction.From,
                To = transaction.To
            });
        }
    }
}
