using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace middleware.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class NetworkController : Controller
    {
        private readonly INetworkControl _networkControl;

        public NetworkController(INetworkControl networkControl)
        {
            _networkControl = networkControl;
        }

        [HttpGet("interfaces")]
        public IActionResult GetNetworkInterfaces()
        {
            var interfaces = _networkControl.GetNetworkInterfaces();
            return Ok(interfaces);
        }
    }
}
