using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Works
                .Include(w => w.User)
                .Include(w => w.ProgrammingLanguage).ToListAsync());
        }

        [HttpGet]
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

            ViewBag.Title = work.Title;

            return View(work);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var work = _context.Works
                .Include(w => w.ProgrammingLanguage).FirstOrDefaultAsync(w => w.ID == id);

            if (work == null)
                return NotFound();

            ViewBag.Lang = new SelectList(await _context.ProgrammingLanguages.ToListAsync(), "ID", "Title");

            return View(work);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Work work, int? id)
        {
            if (id == null)
                return NotFound();

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
    }
}
