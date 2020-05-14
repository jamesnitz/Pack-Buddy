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
using PackBuddy.Models.ViewModels.GearViewModels;

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
        public async Task<ActionResult> Create(int tripId)
        {
            var user = await GetCurrentUserAsync();
            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.Id == tripId);
            var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
               .Include(g => g.GearType)
               .ToListAsync();
            var addedGears = new List<GearViewModel>();
            foreach(var gear in usersGear)
            {
                var newGear = new GearViewModel()
                {
                    Gear = gear
                };
                addedGears.Add(newGear);
            }
            var sharedGear = await _context.SharedGears
                .Include(s => s.Gear)
                .Include(s => s.Gear.GearType)
                .Where(s => s.AcceptedRequest == true)
                .Where(s => s.ApplicationuserId == user.Id)
                .ToListAsync();
            foreach (var gear in sharedGear)
            {
                var gearModel = new GearViewModel()
                {
                    Gear = gear.Gear
                };
                addedGears.Add(gearModel);
            }

            var viewModel = new AddGearTripViewModel()
            {
                Trip = trip,
                Gears = usersGear,
                ApplicationUser = user,
                AddedGears = addedGears
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
                var gearsToAdd = new List<GearViewModel>();
                foreach(var gear in viewModel.AddedGears)
                {
                    if (gear.SelectedGear == true)
                    {
                        gearsToAdd.Add(gear);
                    }
                }


                foreach (var gear in gearsToAdd)
                {
                    AddSingleGearTrip(viewModel.Trip.Id, gear.Gear.Id);
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
               .Include(g => g.GearTrips)
                    .ThenInclude(g => g.Trip)
               .ToListAsync();

            var SelectedGear = new List<Gear>();
            foreach(var item in usersGear)
            {
                foreach(var gt in item.GearTrips)
                {
                    if (gt.TripId == tripId)
                    {
                        SelectedGear.Add(item);
                    }
                }
            }
            var addedGears = new List<GearViewModel>();
            foreach (var gear in usersGear)
            {
                var newGear = new GearViewModel()
                {
                    Gear = gear,
                    TripId = tripId
                };
                addedGears.Add(newGear);
            }
            var sharedGear = await _context.SharedGears
               .Include(s => s.Gear)
               .Include(s => s.Gear.GearType)
               .Include(s => s.Gear.GearTrips)
                    .ThenInclude(s => s.Trip)
               .Where(s => s.AcceptedRequest == true)
               .Where(s => s.ApplicationuserId == user.Id)
               .ToListAsync();
            foreach (var gear in sharedGear)
            {
                    
                var gearModel = new GearViewModel()
                {
                    Gear = gear.Gear,
                    TripId = tripId    
                };
                addedGears.Add(gearModel);
                foreach(var item in gear.Gear.GearTrips)
                {
                    if (item.TripId == tripId)
                    {
                        SelectedGear.Add(gear.Gear);
                    }
                }
            }

            var viewModel = new EditGearTripViewModel()
            {
                Trip = trip,
                AddedGears = addedGears,
                AlreadySelected = SelectedGear,
                ApplicationUser = user
            };
            return View(viewModel);
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
        [HttpPost]  
        public async Task<ActionResult> RemoveFromTrip(string comboId)
        {
            var list = comboId.Split(",");
            int gearId = Int32.Parse(list[0]);
            int tripId = Int32.Parse(list[1]);
            var gearTrip = await _context.GearTrips.Where(gt => gt.TripId == tripId).FirstOrDefaultAsync(gt => gt.GearId == gearId);

            _context.GearTrips.Remove(gearTrip);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "GearTrips", new { tripId = tripId });
        }

        [HttpPost]
        public async Task<ActionResult> AddToTrip(string comboId)
           {
            var list = comboId.Split(",");
            int gearId = Int32.Parse(list[0]);
            int tripId = Int32.Parse(list[1]);

            var gearTripData = new GearTrip()
            {
                GearId = gearId,
                TripId = tripId
            };
            _context.GearTrips.Add(gearTripData);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", "GearTrips", new { tripId = tripId });
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