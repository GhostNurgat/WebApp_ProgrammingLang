using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
    using Data;

    public class MentorsController : Controller
    {
        private readonly ProgLangContext _context;

        public MentorsController(ProgLangContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Преподаватель-редактор, Преподаватель-консультант")]
        public IActionResult Index() => View();

        [Authorize(Roles = "Преподаватель-редактор")]
        public async Task<IActionResult> TaskList(string userName, string searchTask, string language)
        {
            IQueryable<string> langQuary = _context.ProgrammingLanguages.Select(l => l.Title);

            var tasks = await _context.Tasks.Where(t => t.User.UserName == userName)
                .Include(t => t.User).Include(t => t.ProgrammingLanguage).ToListAsync();

            if (!string.IsNullOrEmpty(searchTask))
                tasks = tasks.Where(t => t.Title.Contains(searchTask)).ToList();

            if (!string.IsNullOrEmpty(language))
                tasks = tasks.Where(t => t.ProgrammingLanguage.Title == language).ToList();

            var taskVM = new TaskViewModel
            {
                Tasks = tasks,
                Languages = new SelectList(await langQuary.ToListAsync())
            };

            return PartialView(taskVM);
        }
    }
}
