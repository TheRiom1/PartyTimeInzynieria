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
    public class ThemesController : Controller
    {
        private readonly IThemesService _service;
        public ThemesController(IThemesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allThemes = await _service.GetAllAsync();
            return View(allThemes);
        }
        //Get:Themes/Create

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicture, ThemeName, Description, Price")] Theme theme)
        {
            if (!ModelState.IsValid)
            {
                return View(theme);
            }
            await _service.AddAsync(theme);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        //Get: Themes/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var themeDetails = await _service.GetByIdAsync(id);

            if (themeDetails == null) return View("NotFound");
            return View(themeDetails);
        }
        //Get:Rooms/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var themeDetails = await _service.GetByIdAsync(id);

            if (themeDetails == null) return View("NotFound");
            return View(themeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id, ProfilePicture, ThemeName, Description, Price")] Theme theme)
        {
            if (!ModelState.IsValid) return View(theme);
            if (id == theme.Id)
            {
                await _service.UpdateAsync(id, theme);
                return RedirectToAction(nameof(Index));
            }
            return View(theme);

        }

        //Get:Themes/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var themeDetails = await _service.GetByIdAsync(id);

            if (themeDetails == null) return View("NotFound");
            return View(themeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var themeDetails = await _service.GetByIdAsync(id);
            if (themeDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
