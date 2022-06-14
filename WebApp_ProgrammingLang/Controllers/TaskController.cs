using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
    using Data;

    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ProgLangContext _context;
        private readonly IWebHostEnvironment appEnvironment;

        public TaskController(ILogger<TaskController> logger, ProgLangContext context, IWebHostEnvironment env)
        {
            _logger = logger;
            _context = context;
            appEnvironment = env;
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

        public async Task<IActionResult> Index(int? id, string searchTask)
        {
            if (id == null)
                return NotFound();

            var lang = await _context.ProgrammingLanguages.FindAsync(id);
            if (lang == null)
                return BadRequest();

            var tasks = await _context.Tasks.Where(t => t.LanguageID == lang.ID)
                .Include(t => t.User)
                .Include(t => t.ProgrammingLanguage).ToListAsync();

            if (!string.IsNullOrEmpty(searchTask))
                tasks = tasks.Where(t => t.Title == searchTask).ToList();

            var taskVM = new TaskViewModel
            {
                Tasks = tasks,
                LangID = lang.ID,
            };

            return View(taskVM);
        }

        [Authorize]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _context.Tasks
                .Include(t => t.User).Include(t => t.ProgrammingLanguage).FirstOrDefaultAsync(t => t.ID == id);

            if (task == null)
                return NotFound();

            string TextPath = Path.Combine(appEnvironment.WebRootPath, $"files/tasks/text/{task.FileText}");
            string text = string.Empty;

            using (StreamReader sr = new StreamReader(TextPath))
            {
                text = sr.ReadToEnd();
            }

            ViewBag.Text = text;
            ViewBag.Title = task.Title;

            return View(task);
        }

        [Authorize]
        public IActionResult Download(string filename)
        {
            var TaskPath = Path.Combine(appEnvironment.WebRootPath, "files/tasks/file");
            PhysicalFileProvider fileProvider = new PhysicalFileProvider(TaskPath);

            var file = fileProvider.GetFileInfo(filename);

            if (file.Exists)
                return PhysicalFile(file.PhysicalPath, "application/octet-stream", file.Name);
            else
                return NotFound();
        }
    }
}
