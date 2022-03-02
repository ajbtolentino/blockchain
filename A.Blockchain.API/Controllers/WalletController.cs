using A.Blockchain.API.Models;
using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IQueryDispatcher queryDispatcher;
        private readonly ICommandDispatcher commandDispatcher;

        public WalletController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route(nameof(WalletController.Balance))]
        public async Task<IActionResult> Balance(string address)
        {
            var result = await this.queryDispatcher.QueryAsync(new GetBalanceQuery(address));

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(WalletController.Send))]
        public async Task<IActionResult> Send(SendModel model)
        {
            await this.commandDispatcher.DispatchAsync(new SendCommand(model.FromAddress, 
                                                                       model.ToAddress, 
                                                                       model.Amount));

            return Ok();
        }

        [HttpPost]
        [Route(nameof(WalletController.Fund))]
        public async Task<IActionResult> Fund(FundModel model)
        {
            await this.commandDispatcher.DispatchAsync(new FundCommand(model.ToAddress,
                                                                       model.Amount));

            return Ok();
        }

        [HttpPost]
        [Route(nameof(WalletController.Receive))]
        public IActionResult Receive()
        {
            return Ok();
        }
    }
}
