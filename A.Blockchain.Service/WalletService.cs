using A.Blockchain.Core.Domain;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Transaction;
using A.Blockchain.Core.Interfaces;
using A.Blockchain.Core.Interfaces.Repository;
using A.Blockchain.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Service
{
    public class WalletService : ServiceBase, IWalletService
    {
        private readonly IBlockRepository blockRepository;
        private readonly IRepository<Transaction> pendingTransactionRepository;

        public WalletService(IBlockRepository blockRepository, 
            IRepository<Transaction> transactionRepository, 
            IObjectMapper mapper) : base(mapper)
        {
            this.blockRepository = blockRepository;
            this.pendingTransactionRepository = transactionRepository;
        }

        public ResponseDTO<decimal> GetBalance(string address)
        {
            var blocks = this.blockRepository.GetAll();
            var pendingTransactions = this.pendingTransactionRepository.GetAll();

            var debit = blocks.Sum(_ => _.Transactions.Where(__ => __.From == address).Sum(__ => __.Amount));
            var credit = blocks.Sum(_ => _.Transactions.Where(__ => __.To == address).Sum(__ => __.Amount));

            var pendingDebit = pendingTransactions.Where(__ => __.From == address).Sum(_ => _.Amount);
            var pendingCredit = pendingTransactions.Where(__ => __.To == address).Sum(_ =>_.Amount);

            var total = (credit + pendingCredit) - (pendingDebit + debit);

            return new ResponseDTO<decimal>("Succes", total);
        }

        public ResponseDTO<TransactionDTO> Send(string fromAddress, string toAddress, decimal amount)
        {
            var transaction = this.pendingTransactionRepository.Add(new Transaction
            {
                Amount = amount,
                From = fromAddress,
                To = toAddress,
                Timestamp = DateTime.UtcNow
            });

            return new ResponseDTO<TransactionDTO>("Success", this.Map<TransactionDTO>(transaction));
        }

        public ResponseDTO<TransactionDTO> Fund(string toAddress, decimal amount)
        {
            var transaction = this.pendingTransactionRepository.Add(new Transaction
            {
                Amount = amount,
                From = "System-Fund",
                To = toAddress,
                Timestamp = DateTime.UtcNow
            });

            return new ResponseDTO<TransactionDTO>("Success", this.Map<TransactionDTO>(transaction));
        }
    }
}
