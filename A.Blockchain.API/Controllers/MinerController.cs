<<<<<<< HEAD
﻿using A.Blockchain.API.Models;
using A.Blockchain.Application.Abstractions.Commands;
using A.Blockchain.Application.Abstractions.Queries;
using A.Blockchain.Application.Commands;
using A.Blockchain.Application.DTO;
using A.Blockchain.Application.Queries;
=======
﻿using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
>>>>>>> 1a90bde46e322a7a7dae66cda0192fc1b658bd5e
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinerController : ControllerBase
    {
<<<<<<< HEAD
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
=======
        private readonly INodeService nodeService;
        private readonly IMinerService minerService;

        public MinerController(INodeService nodeService, IMinerService minerService)
        {
            this.nodeService = nodeService;
            this.minerService = minerService;
        }

        [HttpPost]
        public IActionResult Mine()
        {
            //Need structural pattern
            var latestBlock = nodeService.GetLatestBlock();
            var pendingTransactions = nodeService.GetPendingTransactions();
>>>>>>> 1a90bde46e322a7a7dae66cda0192fc1b658bd5e

            var block = new BlockDTO
            {
                Height = latestBlock.Data.Height,
                PreviousHash = latestBlock.Data.Hash,
                Transactions = pendingTransactions.Data,
                Timestamp = DateTime.UtcNow
            };

            this.minerService.Mine(block);

            return Ok(block);
        }
    }
}
