using System;
using A.Blockchain.Application.DTO;

namespace A.Blockchain.Application.Abstractions.Builders
{
    public interface IBlockBuilder : IHashBuilder<IBlockBuilder, BlockDTO>
    {
        IBlockBuilder WithPreviousBlock(BlockDTO previousBlock);
        IBlockBuilder WithTransactions(IEnumerable<TransactionDTO> transactions);
        IBlockBuilder WithValidation();
    }
}

