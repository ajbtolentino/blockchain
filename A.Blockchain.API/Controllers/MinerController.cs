using A.Blockchain.API.Models;
using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.DTO;
using A.Blockchain.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinerController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;

        public MinerController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [Route("/mine")]
        [HttpPost]
        public async Task<IActionResult> Mine(MineBlockModel model)
        {
            var result = await this.commandDispatcher.DispatchAsync<BlockDTO>(
                            new MineBlockCommand(3, model.PreviousHash, model.TransactionIds));

            return Ok(result);
        }
    }
}
