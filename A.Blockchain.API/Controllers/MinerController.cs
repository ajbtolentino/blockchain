using A.Blockchain.Core.DTO;
using A.Blockchain.Core.DTO.Block;
using A.Blockchain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinerController : ControllerBase
    {
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
