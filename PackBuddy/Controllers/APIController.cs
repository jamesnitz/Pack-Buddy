using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PackBuddy.Data;
using PackBuddy.Models;
using static PackBuddy.Models.COPYJSON;

namespace PackBuddy.Controllers
{
    [Authorize]
    public class APIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public APIController(ApplicationDbContext context, UserManager<ApplicationUser> usermanager)
        {
            _userManager = usermanager;
            _context = context;
        }
        // GET: API
        public async Task<ActionResult> Index(string searchString, bool favorite)
        {
            var user = await GetCurrentUserAsync();
            if (favorite == true)
            {
                var gear = new RootGear();
                var results = new List<Results>();
                var favorites = await _context.WishLists.Where(w => w.ApplicationuserId == user.Id).ToListAsync();
            foreach( var wishListItem in favorites)
                {
                    var response = await GetGear(wishListItem.ProductId);
                    results.Add(response.Result);
                }
                gear.Result = results;
                ViewBag.favorite = true;
                return View(gear);
            }
            else
            {

            if (searchString != null)
            {
            var gear = await GetGearRecord(searchString);
                if (gear.Count < 1)
                {
                    ViewBag.noResults = true;
                }
            return View(gear);
            }
            if (searchString == null)
            {
                ViewBag.noSearch = true;
            }
            return View();          
            }
        }

        // GET: API/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // POST: API/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string id)
        {
            try
            {
                var user = await GetCurrentUserAsync();
                var gear = await GetGear(id);
                var wishList = new WishList()
                {
                    Name = gear.Result.Name,
                    ProductId = gear.Result.Id,
                    PrimaryImage = gear.Result.Images.PrimarySmall,
                    Price = gear.Result.FinalPrice,
                    PurchaseLink = gear.Result.AffiliateWebUrl,
                    ApplicationuserId = user.Id
                };
                _context.WishLists.Add(wishList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: API/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: API/Edit/5
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

        // POST: API/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var wishListItem = await _context.WishLists.FirstOrDefaultAsync(w => w.ProductId == id);
                _context.WishLists.Remove(wishListItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        private async Task<RootGear> GetGearRecord(string searchString)
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"https://api.sierra.com/api/1.0/products/search~{searchString}?api_key=a13e35793a797e828b12e82f51d4ba88&perpage=100");
                return JsonConvert.DeserializeObject<RootGear>(content);
            }

        }
        private async Task<Response> GetGear(string id)
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"http://api.sierratradingpost.com/api/1.0/product/{id}/?api_key=a13e35793a797e828b12e82f51d4ba88");
                var response = JsonConvert.DeserializeObject<Response>(content);
                return response;
            }

        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}