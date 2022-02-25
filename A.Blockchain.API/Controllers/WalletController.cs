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
        [Route(nameof(WalletController.Balance))]
        public IActionResult Balance(string address)
        {
            var result = this.walletService.GetBalance(address);

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(WalletController.Create))]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost]
        [Route(nameof(WalletController.Send))]
        public IActionResult Send(SendModel model)
        {
            var result = this.walletService.Send(model.FromAddress, model.ToAddress, model.Amount);

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(WalletController.Fund))]
        public IActionResult Fund(FundModel model)
        {
            var result = this.walletService.Fund(model.ToAddress, model.Amount);

            return Ok(result);
        }

        [HttpPost]
        [Route(nameof(WalletController.Receive))]
        public IActionResult Receive()
        {
            return Ok();
        }
    }
}
