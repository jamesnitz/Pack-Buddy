using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PackBuddy.Data;
using PackBuddy.Models;
using PackBuddy.Models.ViewModels.GearTripViewModels;

namespace PackBuddy.Controllers
{
    public class GearTripsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GearTripsController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: GearTrips
        public ActionResult Index()
        {
            return View();
        }

        // GET: GearTrips/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GearTrips/Create
        public async Task<ActionResult> Create(int tripId)
        {
            var user = await GetCurrentUserAsync();
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
            var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
               .Include(g => g.GearType)
               .ToListAsync();
            var viewModel = new AddGearTripViewModel()
            {
                Trip = trip,
                Gears = usersGear,
                ApplicationUser = user,
            };
            return View(viewModel);
        }

        // POST: GearTrips/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AddGearTripViewModel viewModel)
        {
            try
            {
                foreach (var gear in viewModel.Gears)
                {
                    AddSingleGearTrip(viewModel.Trip.Id, gear.Id);
                }
                // TODO: Add update logic here
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Trips");
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: GearTrips/Edit/5
        public async Task<ActionResult> Edit(int tripId)
        {
            var user = await GetCurrentUserAsync();
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
            var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
               .Include(g => g.GearType)
               .ToListAsync();
            var viewModel = new AddGearTripViewModel()
            {
                Trip = trip,
                Gears = usersGear,
                ApplicationUser = user
            };
            return View(viewModel);
        }

        // POST: GearTrips/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AddGearTripViewModel viewModel)
        {
            try
            {
                foreach(var gear in viewModel.Gears)
                {
                    AddSingleGearTrip(viewModel.Trip.Id, gear.Id);
                }
                // TODO: Add update logic here
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Trips");
            }
            catch
            {
                return View();
            }
        }

        // GET: GearTrips/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GearTrips/Delete/5
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
        private void AddSingleGearTrip(int tripId, int gearId)
        {
            var gearTripData = new GearTrip()
            {
                GearId = gearId,
                TripId = tripId
            };
            _context.GearTrips.Add(gearTripData);

        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}