using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
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

        public async Task<IActionResult> Language(string searchLang, string typeString)
        {
            IQueryable<string> queryType = _context.ProgrammingLanguageTypes.Select(t => t.Title);

            var langs = await _context.ProgrammingLanguages.Include(l => l.Type).ToListAsync();

            if (!string.IsNullOrEmpty(searchLang))
                langs = langs.Where(l => l.Title.Contains(searchLang)).ToList();

            if (!string.IsNullOrEmpty(typeString))
                langs = langs.Where(l => l.Type.Title == typeString).ToList();

            var languageVM = new LanguagesViewModel
            {
                Languages = langs,
                Types = new SelectList(await queryType.ToListAsync())
            };

            return View(languageVM);
        }


    }
}
