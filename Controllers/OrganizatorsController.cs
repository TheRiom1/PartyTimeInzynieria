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
    public class OrganizatorsController : Controller
    {
        private readonly IOrganizatorsService _service;
     
        public OrganizatorsController(IOrganizatorsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allOrganizators = await _service.GetAllAsync();
            return View(allOrganizators);
        }

        [AllowAnonymous]
        //Get: Organizators/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var organizatorDetails = await _service.GetByIdAsync(id);

            if (organizatorDetails == null) return View("NotFound");
            return View(organizatorDetails);
        }

        //Get:Producers/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicture, FirstName, LastName, Description")] Organizator organizator)
        {
            if (!ModelState.IsValid)
            {
                return View(organizator);
            }
            await _service.AddAsync(organizator);
            return RedirectToAction(nameof(Index));
        }
       

        //Get:Organizators/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var organizatorDetails = await _service.GetByIdAsync(id);

            if (organizatorDetails == null) return View("NotFound");
            return View(organizatorDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProfilePicture, FirstName, LastName, Description")] Organizator organizator)
        {
            if (!ModelState.IsValid) return View(organizator);
            if (id == organizator.Id)
            {
                await _service.UpdateAsync(id, organizator);
                return RedirectToAction(nameof(Index));
            }
            return View(organizator);
            
        }

        //Get:Organizators/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var organizatorDetails = await _service.GetByIdAsync(id);

            if (organizatorDetails == null) return View("NotFound");
            return View(organizatorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var organizatorDetails = await _service.GetByIdAsync(id);
            if (organizatorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
