using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
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
    }
}
