using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using Data;

    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ProgLangContext _context;

        public TaskController(ILogger<TaskController> logger, ProgLangContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Language()
        {
            var langs = _context.ProgrammingLanguages.Include(l => l.Type);
            return View(await langs.ToListAsync());
        }
    }
}
