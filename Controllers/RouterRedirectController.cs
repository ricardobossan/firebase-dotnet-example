using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("")]
    public class RouteRedirectController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Redirect("~/swagger");
        }
    }
}
