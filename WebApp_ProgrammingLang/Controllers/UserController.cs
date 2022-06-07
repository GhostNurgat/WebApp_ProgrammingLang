using Microsoft.AspNetCore.Mvc;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using Data;

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ProgLangContext _context;

        public UserController(ILogger<UserController> logger, ProgLangContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult MyProfile(string userName)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return NotFound();

            return View(user);
        }

        public async Task<IActionResult> Info(int id)
        {
            User user = await _context.Users.FindAsync(id);

            if (user == null)
                return BadRequest();

            return PartialView(user);
        }

        public IActionResult Bid() => PartialView();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bid(MentorBid newBid, string userName)
        {
            if (ModelState.IsValid)
            {
                User user = _context.Users.FirstOrDefault(x => x.UserName == userName);
                newBid.UserID = user.Id;

                await _context.MentorBids.AddAsync(newBid);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return PartialView(newBid);
        }
    }
}
