using A.Blockchain.Core.DTO;
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
        [Route("/initialize")]
        public IActionResult Initialize()
        {
            var data = this.blockchainService.Initialize();

            return Ok(data);
        }

        [HttpGet]
        [Route("/get")]
        public IActionResult GetAll()
        {
            var data = this.blockchainService.GetAllBlocks();

            return Ok(data);
        }

        [HttpPost]
        [Route("/add")]
        public IActionResult AddBlock(BlockDTO block)
        {
            var result = this.blockchainService.AddBlock(new RequestDTO<BlockDTO>(block, string.Empty, DateTime.UtcNow));

            return Ok(result);
        }
    }
}
