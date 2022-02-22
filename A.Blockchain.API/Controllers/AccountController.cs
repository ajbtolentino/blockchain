using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace A.Blockchain.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [Route("/create")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpGet]
        [Route("/getAccount")]
        public IActionResult GetAccount()
        {
            return Ok();
        }
    }
}
