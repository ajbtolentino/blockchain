using A.Blockchain.Application.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace A.Blockchain.Application.Commands
{
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task DispatchAsync<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            using var scope = this.serviceProvider.CreateScope();
            var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

            await handler.HandleAsync(command);
        }

        public async Task<TResult> DispatchAsync<TResult>(ICommandResult<TResult> command) where TResult : class
        {
            using var scope = this.serviceProvider.CreateScope();
            var handlerType = typeof(ICommandResultHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            var handler = scope.ServiceProvider.GetRequiredService(handlerType);

            return await (Task<TResult>)handlerType.GetMethod(nameof(ICommandResultHandler<ICommandResult<TResult>, TResult>.HandleAsync))?.Invoke(handler, new[] { command });
        }
    }
}
