using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Data.Services;
using WebApplication3.Data.Static;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class FeaturesController : Controller
    {

        private readonly IFeaturesService _service;
        public FeaturesController(IFeaturesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            //data is just list of featueres taken from the database
            return View(data);
        }

        //Get:Actors/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Description,Price")] Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View (feature);
            }
            await _service.AddAsync(feature);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        //Get: Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var featureDetails = await _service.GetByIdAsync(id);

            if (featureDetails == null) return View("NotFound");
            return View(featureDetails);
        }

        //Get:Actors/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var featureDetails = await _service.GetByIdAsync(id);

            if (featureDetails == null) return View("NotFound");
            return View(featureDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price")] Feature feature)
        {
            if (!ModelState.IsValid)
            {
                return View(feature);
            }
            await _service.UpdateAsync(id, feature);
            return RedirectToAction(nameof(Index));
        }

        //Get:Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var featureDetails = await _service.GetByIdAsync(id);

            if (featureDetails == null) return View("NotFound");
            return View(featureDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var featureDetails = await _service.GetByIdAsync(id);
            if (featureDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
          
            return RedirectToAction(nameof(Index));
        }



    }
}
