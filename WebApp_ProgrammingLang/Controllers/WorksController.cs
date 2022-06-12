using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
    using Data;

    public class WorksController : Controller
    {
        private readonly ProgLangContext _context;

        public WorksController(ProgLangContext context)
        {
            _context = context;
        }

        private bool WorkExists(int id)
            => _context.Works.Any(w => w.ID == id);

        public async Task<IActionResult> Index(string searchWork, string language, int index)
        {
            IQueryable<string> langQuery = _context.ProgrammingLanguages.Select(l => l.Title);

            var works = await _context.Works
                .Include(w => w.User)
                .Include(w => w.ProgrammingLanguage).ToListAsync();

            if (!string.IsNullOrEmpty(searchWork))
                works = works.Where(w => w.Title.Contains(searchWork)).ToList();

            if (!string.IsNullOrEmpty(language))
            {
                switch (index)
                {
                    case 0:
                        works = works.Where(w => w.ProgrammingLanguage.Title == language)
                            .ToList();
                        break;
                    case 1:
                        works = works.Where(w => w.ProgrammingLanguage.Title == language)
                            .OrderBy(w => w.PublishDate).ToList();
                        break;
                    case 2:
                        works = works.Where(w => w.ProgrammingLanguage.Title == language)
                            .OrderByDescending(w => w.PublishDate).ToList();
                        break;
                    default:
                        works = works.Where(w => w.ProgrammingLanguage.Title == language)
                            .ToList();
                        break;
                }
            }

            var worksVM = new WorksViewModel
            {
                Works = works,
                Languages = new SelectList(await langQuery.ToListAsync())
            };

            return View(worksVM);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            ViewBag.Work = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Work work, string userName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user == null)
                return NotFound();

            work.UserID = user.Id;
            work.PublishDate = DateTime.Now;

            await _context.Works.AddAsync(work);
            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Works");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            ViewBag.Work = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");

            return View(work);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> WorkDetail(int id)
        {
            var work = await _context.Works.Include(w => w.User)
                .Include(w => w.ProgrammingLanguage).FirstOrDefaultAsync(w => w.ID == id);

            if (work == null)
                return NotFound();

            var comments = await _context.WorkComments.Where(c => c.WorkID == work.ID)
                .Include(c => c.User).ToListAsync();

            ViewBag.Title = work.Title;

            var workVM = new WorksViewModel
            {
                Work = work,
                Comments = comments
            };

            return View(workVM);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var work = await _context.Works
                .Include(w => w.ProgrammingLanguage)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.ID == id);

            if (work == null)
                return NotFound();

            ViewBag.Work = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Work work, string userName)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            work.User = user;

            _context.Works.Update(work);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("WorkDetail", "Works", work.ID);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (WorkExists(work.ID))
                    return NotFound();
                else
                    throw;
            }

            return View(work);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var work = await _context.Works.FindAsync(id);
            if (work == null)
                return NotFound();

            return PartialView(work);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var work = await _context.Works.FindAsync(id);
            _context.Works.Remove(work);

            try
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Works");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
