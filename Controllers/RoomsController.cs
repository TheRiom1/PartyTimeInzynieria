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
    public class RoomsController : Controller
    {
        private readonly IRoomsService _service;
        public RoomsController(IRoomsService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allRooms = await _service.GetAllAsync();
            return View(allRooms);
        }
        //Get:Producers/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Picture, Name, Description, Price")] Room room)
        {
            if (!ModelState.IsValid)
            {
                return View(room);
            }
            await _service.AddAsync(room);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        //Get: Rooms/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var roomDetails = await _service.GetByIdAsync(id);

            if (roomDetails == null) return View("NotFound");
            return View(roomDetails);
        }
        //Get:Rooms/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var roomDetails = await _service.GetByIdAsync(id);

            if (roomDetails == null) return View("NotFound");
            return View(roomDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Picture, Name, Description, Price")] Room room)
        {
            if (!ModelState.IsValid) return View(room);
            if (id == room.Id)
            {
                await _service.UpdateAsync(id, room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);

        }

        //Get:Rooms/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var roomDetails = await _service.GetByIdAsync(id);

            if (roomDetails == null) return View("NotFound");
            return View(roomDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomDetails = await _service.GetByIdAsync(id);
            if (roomDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
