using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mottrist.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("hi")]
        public IActionResult GetMyName()
        {
            return Ok("Hi");
        }
    }
}
