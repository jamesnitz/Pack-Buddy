using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static PackBuddy.Models.COPYJSON;

namespace PackBuddy.Controllers
{
    [Authorize]
    public class APIController : Controller
    {
        // GET: API
        public async Task<ActionResult> Index(string searchString)
        {
            if (searchString != null)
            {
                var gear = await GetGearRecord(searchString);
                return View(gear);
            }
            else
            {
                var gear = await GetGear();
                return View(gear);
            }
        }

        // GET: API/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: API/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: API/Create
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

        // GET: API/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: API/Delete/5
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
        private async Task<RootGear> GetGearRecord(string searchString)
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"https://api.sierra.com/api/1.0/products/search~{searchString}?api_key=a13e35793a797e828b12e82f51d4ba88");
                return JsonConvert.DeserializeObject<RootGear>(content);
            }

        }
        private async Task<RootGear> GetGear()
        {
            using (var client = new HttpClient())
            {
                var content = await client.GetStringAsync($"https://api.sierra.com/api/1.0/products/?api_key=a13e35793a797e828b12e82f51d4ba88&page=2");
                return JsonConvert.DeserializeObject<RootGear>(content);
            }

        }
    }
}