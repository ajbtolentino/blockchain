using A.Blockchain.Application.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeService nodeService;

        public NodeController(INodeService nodeService)
        {
            this.nodeService = nodeService;
        }

        [HttpGet]
        [Route(nameof(NodeController.GetAllBlocks))]
        public IActionResult GetAllBlocks()
        {
            var data = this.nodeService.GetAllBlocks();
            
            return Ok(data);
        }

        [HttpGet]
        [Route(nameof(NodeController.GetAllPendingTransactions))]
        public IActionResult GetAllPendingTransactions()
        {
            var data = this.nodeService.GetPendingTransactions();

            return Ok(data);
        }

        [HttpPost]
        public IActionResult AddBlock(BlockDTO block)
        {
            //Validate
            var result = this.nodeService.AddBlock(block);

            return Ok(result);
        }
    }
}

