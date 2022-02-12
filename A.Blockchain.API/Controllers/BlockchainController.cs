using A.Blockchain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockchainController : ControllerBase
    {
        private readonly IBlockchainService blockchainService;

        public BlockchainController(IBlockchainService blockchainService)
        {
            this.blockchainService = blockchainService;
        }

        [HttpGet]
        [Route("/creategenesis")]
        public IActionResult Create()
        {
            this.blockchainService.CreateGenesisBlock();

            return Ok();
        }
    }
}
