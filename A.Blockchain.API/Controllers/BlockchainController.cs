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
        [Route("/addBlock")]
        public IActionResult AddBlock()
        {
            var result = this.blockchainService.AddBlock(new RequestDTO<BlockDTO>(new BlockDTO(), string.Empty, DateTime.UtcNow));
            return Ok();
        }

        [HttpGet]
        [Route("/getAll")]
        public IActionResult GetAll()
        {
            var data = this.blockchainService.ToList();

            return Ok(new ResponseDTO<IEnumerable<BlockDTO>>("Success", data));
        }
    }
}
