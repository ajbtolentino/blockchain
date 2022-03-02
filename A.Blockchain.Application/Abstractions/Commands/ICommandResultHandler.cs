using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Abstractions.Commands
{
    public interface ICommandHandler<TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }

    public interface ICommandResultHandler<in TCommand, TResult> where TCommand : class, ICommandResult<TResult>
    {
        Task<TResult> HandleAsync(TCommand command);
    }
}
