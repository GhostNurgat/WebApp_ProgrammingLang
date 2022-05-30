using Microsoft.AspNetCore.Mvc;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login() => View();
    }
}
