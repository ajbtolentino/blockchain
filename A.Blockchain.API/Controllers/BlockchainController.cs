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

        [HttpPost]
        [Route("/addTransaction")]
        public IActionResult AddTransaction(TransactionDTO request)
        {
            var result = this.blockchainService.AddTransaction(new RequestDTO<TransactionDTO>(request, string.Empty, DateTime.Now));

            return Ok(result);
        }

        [HttpGet]
        [Route("/getLatestBlock")]
        public IActionResult GetLatestBlock()
        {
            var result = this.blockchainService.GetLatestBlock();

            return Ok(result);
        }

        [HttpGet]
        [Route("/getAll")]
        public IActionResult GetAll()
        {
            var result = this.blockchainService.GetAll();

            return Ok(result);
        }
    }
}
