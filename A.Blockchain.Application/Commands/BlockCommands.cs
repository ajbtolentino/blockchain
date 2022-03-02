using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Commands
{
    public record AddBlockCommand(string hash, string previousHash, int nonce, int[] transactionId) : ICommand;

    public record MineBlockCommand(int difficulty, string previousHash, int[] transactions) : ICommandResult<BlockDTO>;
}
