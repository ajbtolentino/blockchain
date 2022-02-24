using A.Blockchain.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinerController : ControllerBase
    {
        private readonly IMinerService minerService;

        public MinerController(IMinerService minerService)
        {
            this.minerService = minerService;
        }

        [HttpPost]
        [Route("/mine")]
        public IActionResult Mine(int[] transactionIds)
        {
            var result = this.minerService.Mine(transactionIds);

            return Ok(result);
        }
    }
}
