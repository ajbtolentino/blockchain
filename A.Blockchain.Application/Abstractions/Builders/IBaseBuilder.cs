using System;
namespace A.Blockchain.Application.Abstractions.Builders
{
    public interface IHashBuilder<TBuilder,TResult>
    {
        TBuilder WithConfiguration();
        TBuilder WithHash();
        TResult Build();
    }
}

