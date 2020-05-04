using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackBuddy.Data;
using PackBuddy.Models;

namespace PackBuddy.Controllers
{
    [Authorize]
    public class GearsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GearsController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: Gears
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
                .Include(g => g.GearType)
                .ToListAsync();
            return View(usersGear);
        }

        // GET: Gears/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Gears/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gears/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Gears/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gears/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Gears/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {   
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}