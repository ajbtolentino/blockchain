using System;
using A.Blockchain.Application.DTO;

namespace A.Blockchain.Application.Abstractions.Builders
{
    public interface ITransactionBuilder : IHashBuilder<ITransactionBuilder, TransactionDTO>
    {
        ITransactionBuilder WithDetails(string from, string to, decimal amount);
        ITransactionBuilder SkipValidation();
    }
}

