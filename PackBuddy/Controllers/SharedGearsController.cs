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
            var user = await GetCurrentUserAsync();
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

            var requestsRecieved = await _context.SharedGears
                .Include(g => g.Gear)
                .Include(g => g.ApplicationUser)
                .Where(g => g.Gear.ApplicationuserId == user.Id)
                .Where(g => g.AcceptedRequest == false)
                .ToListAsync();
            viewModel.RequestsReceived = requestsRecieved;
            return View(viewModel);
        }

        // GET: SharedGears/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: SharedGears/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(int gearId)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var request = new SharedGear()
                {
                    ApplicationuserId = user.Id,
                    GearId = gearId,
                    RequestMessage = null,
                    AcceptedRequest = false
                };

                var requestedGear = await _context.Gears
                    .Include(g => g.ApplicationUser)
                    .FirstOrDefaultAsync(g => g.Id == gearId);
                _context.SharedGears.Add(request);
                await _context.SaveChangesAsync();
                var sharedGear = await _context.SharedGears.FirstOrDefaultAsync(g => g.GearId == gearId && g.ApplicationuserId == user.Id && g.RequestMessage == null);
            TempData["requestCreated"] = "Your Request has been sent.";
            TempData["gearId"] = sharedGear.Id;
            return RedirectToAction("Index", "SharedGears", new { searchString = requestedGear.ApplicationUser.Email});
            }
            catch
            {
                return View();
            }
        }

        // GET: SharedGears/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var sharedGear = await _context.SharedGears.FirstOrDefaultAsync(s => s.Id == id);
            return View(sharedGear);
        }

        // POST: SharedGears/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, SharedGear sharedGear)
        {
            try
            {
                _context.Update(sharedGear);
                await _context.SaveChangesAsync();

                TempData["messageSent"] = "Your message has been sent.";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AcceptRequest(int requestId)
            {
            try
            {
                var foundGear = await _context.SharedGears.FirstOrDefaultAsync(s => s.Id == requestId);
                foundGear.AcceptedRequest = true;
                _context.SharedGears.Update(foundGear);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return View();
            }
        }
        // POST: SharedGears/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int requestId)
        {
            try
            {
                var foundGear = await _context.SharedGears.FirstOrDefaultAsync(s => s.Id == requestId);
                _context.SharedGears.Remove(foundGear);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
               
            }
            catch(Exception ex)
            {
                return View();
            }
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}