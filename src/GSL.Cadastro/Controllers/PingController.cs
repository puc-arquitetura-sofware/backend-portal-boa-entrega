using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GSL.Cadastro.Api.Controllers
{
    [Route("api/ping")]
    [ApiController]
    public class PingController : MainController
    {
        [HttpGet()]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult HealthCheck()
        {
            return Ok();
        }
    }
}
