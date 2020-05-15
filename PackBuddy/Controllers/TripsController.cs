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
    public class TripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public TripsController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: Trips
        public async Task<ActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var usersTrips = await _context.Trips.Where(t => t.ApplicationuserId == user.Id)
               .Include(t => t.GearTrips)
                   .ThenInclude(t => t.Gear)
               .ToListAsync();
            usersTrips.Sort((t1, t2) => DateTime.Compare(t1.EndDate, t2.EndDate));
            usersTrips.Reverse();
            return View(usersTrips);
        }

        // GET: Trips/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var foundTrip = await _context.Trips
                .Include(t => t.GearTrips)
                    .ThenInclude(t => t.Gear)
                    .ThenInclude(g => g.GearType)
                .FirstOrDefaultAsync(t => t.Id == id);

            return View(foundTrip);
        }

        // GET: Trips/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Trip trip)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                trip.ApplicationuserId = user.Id;
                _context.Trips.Add(trip);
                await _context.SaveChangesAsync();

                TempData["tripCreated"] = "Your trip has been created.";
                TempData["tripId"] = trip.Id;
                return RedirectToAction("Index", "Trips");
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        public ActionResult TripCreated()
        {
            return View();
        }
        // GET: Trips/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var foundTrip = await _context.Trips
             .Include(t => t.GearTrips)
                 .ThenInclude(t => t.Gear)
             .FirstOrDefaultAsync(t => t.Id == id);
            return View(foundTrip);
        }

        // POST: Trips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Trip trip)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                trip.ApplicationuserId = user.Id;
                 _context.Trips.Update(trip);
                await _context.SaveChangesAsync();
               

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Trips/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Trips/Delete/5
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