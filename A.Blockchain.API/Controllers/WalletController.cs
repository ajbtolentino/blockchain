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
<<<<<<< HEAD
        [Route("/balance")]
        public async Task<IActionResult> Balance(string address)
=======
        [Route(nameof(WalletController.Balance))]
        public IActionResult Balance(string address)
>>>>>>> 1a90bde46e322a7a7dae66cda0192fc1b658bd5e
        {
            var result = await this.queryDispatcher.QueryAsync(new GetBalanceQuery(address));

            return Ok(result);
        }

        [HttpPost]
<<<<<<< HEAD
        [Route("/send")]
        public async Task<IActionResult> Send(SendModel model)
=======
        [Route(nameof(WalletController.Create))]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost]
        [Route(nameof(WalletController.Send))]
        public IActionResult Send(SendModel model)
>>>>>>> 1a90bde46e322a7a7dae66cda0192fc1b658bd5e
        {
            await this.commandDispatcher.DispatchAsync(new SendCommand(model.FromAddress, 
                                                                       model.ToAddress, 
                                                                       model.Amount));

            return Ok();
        }

        [HttpPost]
<<<<<<< HEAD
        [Route("/fund")]
        public async Task<IActionResult> Fund(FundModel model)
=======
        [Route(nameof(WalletController.Fund))]
        public IActionResult Fund(FundModel model)
>>>>>>> 1a90bde46e322a7a7dae66cda0192fc1b658bd5e
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
