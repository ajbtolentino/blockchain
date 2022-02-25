using System;
namespace A.Blockchain.Core.DTO.Transaction
{
    public class SignedTransaction : TransactionDTO
    {
        public string Sign { get; set; }
    }
}

