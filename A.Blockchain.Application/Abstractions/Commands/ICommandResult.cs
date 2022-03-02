using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Blockchain.Application.Abstractions.Commands
{
    public interface ICommand { }

    public interface ICommandResult<TResult> : ICommand { }
}
