using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace WebApp_ProgrammingLang.Controllers
{
    using Models;
    using ViewModels;
    using Data;

    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly ProgLangContext _context;
        private readonly UserManager<User> _userManager;

        public UserController(ILogger<UserController> logger, ProgLangContext context, UserManager<User> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        private bool UserExists(int id) =>
            _context.Users.Any(u => u.Id == id);

        public IActionResult Profile(string userName)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> Info(int? id)
        {
            if (id == null)
                return NotFound();

            User user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return PartialView(user);
        }

        [Authorize]
        public async Task<IActionResult> Works(int? id)
        {
            if (id == null)
                return NotFound();

            User user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            var works = await _context.Works.Where(w => w.UserID == user.Id)
                .Include(w => w.User).Include(w => w.ProgrammingLanguage).Include(w => w.Comments).ToListAsync();

            return PartialView(works);
        }

        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
                return NotFound();

            User user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        [Authorize]
        public async Task<IActionResult> EditInfo(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
                return NotFound();

            EditUserViewModel editUserVM = new EditUserViewModel
            {
                UserName = user.UserName,
                Surname = user.Surname,
                Name = user.Name,
                BirthDate = user.BirthDate,
                About = user.About
            };

            return PartialView(editUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(EditUserViewModel editUserVM)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(editUserVM.UserName);

                if (user != null)
                {
                    user.Surname = editUserVM.Surname;
                    user.Name = editUserVM.Name;
                    user.BirthDate = (DateTime)editUserVM.BirthDate;
                    user.About = editUserVM.About;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Profile", "User", user.Id);
                    else
                        foreach (var error in result.Errors)
                            ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(editUserVM);
        }

        public async Task<IActionResult> EditImage(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return PartialView(user);
        }

        public async Task<IActionResult> EditImage(User user, int id)
        {
            if (user.Id != id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(user).State = EntityState.Modified;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction("MyProfile", "User", user.UserName);
            }

            return PartialView(user);
        }

        public IActionResult Bid(string userName)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            ViewBag.User = user.Id;
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Bid(MentorBid newBid, string userName)
        {
            User user = _context.Users.FirstOrDefault(x => x.UserName == userName);
            newBid.UserID = user.Id;

            if (ModelState.IsValid)
            {
                await _context.MentorBids.AddAsync(newBid);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return PartialView(newBid);
        }
    }
}
