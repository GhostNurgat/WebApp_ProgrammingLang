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

        [Authorize]
        public async Task<IActionResult> UploadWork(int id, string userName)
        {
            ViewBag.Task = await _context.Tasks.FindAsync(id);
            ViewBag.User = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UploadWork(TaskWorkViewModel model, int id, string userName, IFormFile uploadFile)
        {
            var task = await _context.Tasks.FindAsync(id);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            model.TaskID = task.ID;
            model.UserID = user.Id;

            var taskWork = new TaskWork
            {
                TaskID = task.ID,
                UserID = user.Id,
                DateLoad = DateTime.Now
            };

            if (uploadFile != null)
            {
                string path = "/files/tasks/works/" + uploadFile.FileName;
                using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fs);
                }

                model.Filename = uploadFile.FileName;
            }

            taskWork.IsCompleted = model.IsComplited;
            taskWork.Filename = model.Filename;
            taskWork.Message = model.Message;

            try
            {
                await _context.TaskWorks.AddAsync(taskWork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Detail", "Task", task.ID);
            }
            catch (DbUpdateException)
            {
                throw;
            }
        }

        [Authorize(Roles = "Преподаватель-редактор")]
        [HttpGet]
        public async Task<IActionResult> NewTask()
        {
            ViewBag.Languages = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");
            return View();
        }

        [Authorize(Roles = "Преподаватель-редактор")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewTask(EditTaskViewModel model, IFormFile uploadFileText, IFormFile uploadFile, string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
                return NotFound();

            Task task = new Task
            {
                Title = model.Title,
                UserID = user.Id,
                LanguageID = model.LanguageID,
                DatePublish = DateTime.Now,
                ProgrammingLanguageID = model.LanguageID
            };

            if (uploadFileText != null)
            {
                string path = "/files/tasks/text/" + uploadFileText.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFileText.CopyToAsync(fileStream);
                }

                model.FileText = uploadFileText.FileName;
            }

            if (uploadFile != null)
            {
                string path = "/files/tasks/file/" + uploadFile.FileName;
                using (var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fileStream);
                }

                model.Filename = uploadFile.FileName;
            }

            task.FileText = model.FileText;
            task.FileTask = model.Filename;

            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        [Authorize(Roles = "Преподаватель-редактор")]
        [HttpGet]
        public async Task<IActionResult> EditTask(int? id)
        {
            if (id == null)
                return NotFound();

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
                return NotFound();

            var editTaskVM = new EditTaskViewModel
            {
                Title = task.Title,
                LanguageID = task.ProgrammingLanguageID,
                UserID = task.UserID,
                FileText = task.FileText,
                Filename = task.FileTask,
                PublishDate = task.DatePublish
            };

            ViewBag.Languages = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");
            ViewBag.ID = task.ID;

            return View(editTaskVM);
        }

        [Authorize(Roles = "Преподаватель-редактор")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTask(int id, EditTaskViewModel model, IFormFile uploadFileText, IFormFile uploadFile)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (uploadFileText != null)
            {
                string path = "/files/tasks/text/";

                if (model.FileText == uploadFileText.FileName)
                {
                    path += uploadFileText.FileName;
                    using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFileText.CopyToAsync(fs);
                    }
                }
                else
                {
                    path += uploadFileText.FileName;
                    string oldPath = Path.Combine(appEnvironment.WebRootPath, $"files/tasks/text/{model.FileText}");

                    System.IO.File.Delete(oldPath);

                    using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFileText.CopyToAsync(fs);
                    }

                    model.FileText = uploadFileText.FileName;
                }
            }

            if (uploadFile != null)
            {
                string path = "/files/tasks/file/";

                if (uploadFile.FileName == model.Filename)
                {
                    path += uploadFile.FileName;
                    using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fs);
                    }
                }
                else
                {
                    string oldPath = Path.Combine(appEnvironment.WebRootPath, $"files/tasks/file/{model.Filename}");
                    path += uploadFile.FileName;

                    System.IO.File.Delete(oldPath);

                    using (var fs = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fs);
                    }

                    model.Filename = uploadFile.FileName;
                }
            }

            task.Title = model.Title;
            task.LanguageID = model.LanguageID;
            task.ProgrammingLanguageID = model.LanguageID;
            task.UserID = model.UserID;
            task.DatePublish = (DateTime)model.PublishDate;
            task.FileText = model.FileText;
            task.FileTask = model.Filename;

            try
            {
                _context.Tasks.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
