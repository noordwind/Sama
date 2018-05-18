using Sama.Services.Dispatchers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Sama.Api.Controllers
{
    [Route("")]
    public class HomeController : BaseController
    {
        public HomeController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get() => Ok("Sama API");
    }
}