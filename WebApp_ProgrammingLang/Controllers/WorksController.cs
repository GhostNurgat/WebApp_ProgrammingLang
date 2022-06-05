using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using Data;

    public class WorksController : Controller
    {
        public IActionResult Index() => View();
    }
}
