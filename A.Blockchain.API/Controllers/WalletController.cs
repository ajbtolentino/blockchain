using A.Blockchain.API.Models;
using A.Blockchain.Core.DTO;
using A.Blockchain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IBlockchainService blockchainService;

        public WalletController(IBlockchainService blockchainService)
        {
            this.blockchainService = blockchainService;
        }

        [HttpGet]
        [Route("/balance")]
        public IActionResult Balance()
        {
            return Ok();
        }

        [HttpPost]
        [Route("/send")]
        public IActionResult Send(SendModel model)
        {
            var result = this.blockchainService.AddTransaction(new RequestDTO<TransactionDTO>(new TransactionDTO
            {
                Amount = model.Amount,
                From = model.FromAddress,
                To = model.ToAddress
            }, string.Empty, DateTime.UtcNow));

            return Ok(result);
        }

        [HttpPost]
        [Route("/receive")]
        public IActionResult Receive()
        {
            return Ok();
        }
    }
}
