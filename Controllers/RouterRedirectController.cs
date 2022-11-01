using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
