using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackBuddy.Data;
using PackBuddy.Models;
using PackBuddy.Models.ViewModels.SharedGearsViewModels;

namespace PackBuddy.Controllers
{
    [Authorize]
    public class SharedGearsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SharedGearsController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: SharedGears
        public async Task<ActionResult> Index(string searchString)
        {
            var gears = new List<Gear>();
            if (searchString != null)
            {
                gears = await _context.Gears.Where(g => g.ApplicationUser.Email == searchString)
                .Include(g => g.GearType)
                .ToListAsync();
            }

            var viewModel = new SharedGearIndexViewModel()
            {
                FriendGears = gears
            };
            if (gears.Count < 1)
            {
                ViewBag.nothingFound = true;

            }
            return View(viewModel);
        }

        // GET: SharedGears/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SharedGears/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SharedGears/Create
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

        // GET: SharedGears/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SharedGears/Edit/5
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

        // GET: SharedGears/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SharedGears/Delete/5
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
    }
}