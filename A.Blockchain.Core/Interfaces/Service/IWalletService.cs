using A.Blockchain.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Core.Interfaces.Service
{
    public interface IWalletService
    {
        ResponseDTO<decimal> GetBalance(string address);

        ResponseDTO<TransactionDTO> Send(string fromAddress, string toAddress, decimal amount);

        ResponseDTO<TransactionDTO> Fund(string toAddress, decimal amount);
    }
}
