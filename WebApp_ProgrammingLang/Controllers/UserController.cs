﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        private bool UserExists(int id) =>
            _context.Users.Any(u => u.Id == id);

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
                return NotFound();

            return PartialView(user);
        }

        public async Task<IActionResult> Update(int id)
        {
            User user = await _context.Users.FindAsync(id);

            if (user == null)
                return NotFound();

            return View(user);
        }

        public async Task<IActionResult> EditInfo(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return PartialView(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInfo(int id, [Bind("Id,Surname,Name,BirthDate,About")] User user)
        {
            if (id != user.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(user).State = EntityState.Modified;
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
