using Microsoft.AspNetCore.Mvc;
using middleware.DataBase;

namespace middleware.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataLogController : Controller
    {
        private readonly Iws_db _db;

        public DataLogController(Iws_db Iws_db)
        {
            _db = Iws_db;
        }

        [HttpGet("Getlog")]
        public IActionResult GetNetworkInterfaces()
        {
            var data = _db.GetTagLogs(1,DateTime.Now.AddDays(-8),DateTime.Now);
            return Ok(data);
        }
    }
}
