using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Handlers
{
    internal class WalletCommandHandlers : ICommandHandler<SendCommand>, 
                                           ICommandHandler<FundCommand>
    {
        public async Task HandleAsync(SendCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task HandleAsync(FundCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
