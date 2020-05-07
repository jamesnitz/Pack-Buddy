using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PackBuddy.Data;
using PackBuddy.Models;
using PackBuddy.Models.ViewModels.GearViewModels;

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
        public async Task<ActionResult> Index(string searchString, int filter, bool borrow)
        {
            var gearOptions = await _context.GearTypes.Select(g => new SelectListItem() { Text = g.Label, Value = g.Id.ToString() })
               .ToListAsync();
            ViewBag.ProductTypes = gearOptions;
            var user = await GetCurrentUserAsync();
            if (searchString != null)
            {
                var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
                   .Where(g => g.Name.Contains(searchString))
                   .Include(g => g.GearType)
                   .ToListAsync();
                if (usersGear.Count < 1)
                {
                    ViewBag.nothingFound = true;
                }
                return View(usersGear);

            }
            else if (filter != 0)
            {
                var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
                 .Where(g => g.GearTypeId == filter)
                 .Include(g => g.GearType)
                 .ToListAsync();
                if (usersGear.Count < 1)
                {
                    ViewBag.nothingFound = true;
                }
                return View(usersGear);

            }
            else if (borrow == true)
            {
                var sharedGear = await _context.SharedGears
                    .Include(s => s.Gear)
                    .Include(s => s.Gear.GearType)
                    .Where(s => s.AcceptedRequest == true)
                    .Where(s => s.ApplicationuserId == user.Id)
                    .ToListAsync();
                var usersGear = new List<Gear>();
                foreach(var gear in sharedGear)
                {
                    usersGear.Add(gear.Gear);
                }

                return View(usersGear);
            }
            else
            {

            var usersGear = await _context.Gears.Where(g => g.ApplicationuserId == user.Id)
                .Include(g => g.GearType)
                .ToListAsync();
            return View(usersGear);
            }
        }

        // GET: Gears/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Gears/Create
        public async Task<ActionResult> Create()
        {
            var gearOptions = await _context.GearTypes.Select(g => new SelectListItem() { Text = g.Label, Value = g.Id.ToString() })
                .ToListAsync();
            var viewModel = new GearFormViewModel();
            viewModel.GearTypeOptions = gearOptions;
            return View(viewModel);
        }

        // POST: Gears/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GearFormViewModel gearFormView)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var gearData = new Gear()
                {
                    Name = gearFormView.Name,
                    Condtion = gearFormView.Condtion,
                    ApplicationuserId = user.Id,
                    Description = gearFormView.Description,
                    Rating = gearFormView.Rating,
                    GearTypeId = gearFormView.GearTypeId,
                };

                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
                if (gearFormView.ImagePath != null)
                {
                    var fileName = Guid.NewGuid().ToString() + gearFormView.ImagePath.FileName;
                    gearData.ImagePath = fileName;
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        await gearFormView.ImagePath.CopyToAsync(fileStream);
                    }
                }
                _context.Gears.Add(gearData);
                await _context.SaveChangesAsync();

                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Gears/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var gearOptions = await _context.GearTypes.Select(g => new SelectListItem() { Text = g.Label, Value = g.Id.ToString() })
               .ToListAsync();
            var gear = await _context.Gears.FirstOrDefaultAsync(g => g.Id == id);
            var viewModel = new GearFormViewModel()
            {
                Name = gear.Name,
                Condtion = gear.Condtion,
                Description = gear.Description,
                GearTypeId = gear.GearTypeId,
                Rating = gear.Rating,
                GearTypeOptions = gearOptions,
            };
            if (gear.ImagePath != null)
            {
                   
            }
    
            return View(viewModel);
        }

        // POST: Gears/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, GearFormViewModel gearFormView)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var gearData = new Gear()
                {
                    Id = id,
                    Name = gearFormView.Name,
                    Condtion = gearFormView.Condtion,
                    ApplicationuserId = user.Id,
                    Description = gearFormView.Description,
                    Rating = gearFormView.Rating,
                    GearTypeId = gearFormView.GearTypeId,
                };
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images");
                if (gearFormView.ImagePath != null)
                {
                    var fileName = Guid.NewGuid().ToString() + gearFormView.ImagePath.FileName;
                    gearData.ImagePath = fileName;
                    using (var fileStream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        await gearFormView.ImagePath.CopyToAsync(fileStream);
                    }
                }

                _context.Gears.Update(gearData);
                await _context.SaveChangesAsync();
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }



        // POST: Gears/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var gear = await _context.Gears.FirstOrDefaultAsync(g => g.Id == id);
                _context.Gears.Remove(gear);
                await _context.SaveChangesAsync();
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