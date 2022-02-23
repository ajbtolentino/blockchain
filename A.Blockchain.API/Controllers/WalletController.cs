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
        private readonly IWalletService walletService;

        public WalletController(IWalletService walletService)
        {
            this.walletService = walletService;
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
            var result = this.walletService.Send(model.FromAddress, model.ToAddress, model.Amount);

            return Ok(result);
        }

        [HttpPost]
        [Route("/fund")]
        public IActionResult Fund(FundModel model)
        {
            var result = this.walletService.Fund(model.ToAddress, model.Amount);

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
