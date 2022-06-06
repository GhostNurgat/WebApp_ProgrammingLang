using Microsoft.AspNetCore.Mvc;

namespace WebApp_ProgrammingLang.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Bid() => PartialView();
    }
}
