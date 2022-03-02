using A.Blockchain.Application.Abstractions.Commands;

namespace A.Blockchain.Application.Commands
{
    public record SendCommand(string from, string to, decimal amount) : ICommand;

    public record FundCommand(string to, decimal amount) : ICommand;
}
