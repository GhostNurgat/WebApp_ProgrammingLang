using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
    using Data;

    public class MentorsController : Controller
    {
        private readonly ProgLangContext _context;
        private readonly IWebHostEnvironment appEnvironment;

        public MentorsController(ProgLangContext context, IWebHostEnvironment env)
        {
            _context = context;
            appEnvironment = env;
        }

        [Authorize(Roles = "Преподаватель-редактор, Преподаватель-консультант")]
        public IActionResult Index() => View();

        [Authorize(Roles = "Преподаватель-редактор")]
        [HttpGet]
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

        [Authorize(Roles = "Преподаватель-редактор, Преподаватель-консультант")]
        [HttpGet]
        public async Task<IActionResult> WorksList(string searchUser)
        {
            var taskWorks = await _context.TaskWorks
                .Include(w => w.Task).Include(w => w.User).ToListAsync();

            if (!string.IsNullOrEmpty(searchUser))
                taskWorks = taskWorks.Where(w => w.User.UserName.Contains(searchUser)).ToList();

            var studentWorkVM = new StudentsWorkViewModel
            {
                TaskWorks = taskWorks
            };

            return PartialView(studentWorkVM);
        }

        [Authorize(Roles = "Преподаватель-редактор, Преподаватель-консультант")]
        public IActionResult DownloadWork(string filename)
        {
            string WorkPath = Path.Combine(appEnvironment.WebRootPath, "files/tasks/works");
            PhysicalFileProvider fileProvider = new PhysicalFileProvider(WorkPath);

            var file = fileProvider.GetFileInfo(filename);

            if (file.Exists)
                return PhysicalFile(file.PhysicalPath, "application/octet-stream", file.Name);
            else
                return NotFound();
        }
    }
}
