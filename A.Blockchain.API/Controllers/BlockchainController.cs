using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public BlockchainController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("/initialize")]
        public IActionResult Initialize()
        {
            //var data = this.commandDispatcher.Initialize();

            return Ok();
        }

        [HttpGet]
        [Route("/getAllBlocks")]
        public async Task<IActionResult> GetAllBlocks()
        {
            var result = await this.queryDispatcher.QueryAsync(new GetAllBlocksQuery());

            return Ok(result);
        }

        [HttpGet]
        [Route("/getAllPendingTransactions")]
        public async Task<IActionResult> GetAllPendingTransactions()
        {
            var result = await this.queryDispatcher.QueryAsync(new GetPendingTransactionsQuery());

            return Ok(result);
        }

        [HttpPost]
        [Route("/add")]
        public async Task<IActionResult> AddBlock(AddBlockCommand block)
        {
            await this.commandDispatcher.DispatchAsync(block);

            return Ok();
        }
    }
}
