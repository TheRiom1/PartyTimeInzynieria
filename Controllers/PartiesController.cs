using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Data.Services;
using WebApplication3.Models;


namespace WebApplication3.Controllers
{
    [Authorize]
    public class PartiesController : Controller
    {
    
        private readonly IPartiesService _service;
        public PartiesController(IPartiesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public IActionResult Home()
        {
            return View("Anonym");
        }

        //????? co to robi
        public async Task<IActionResult> Index()
        {
            var allParties = await _service.GetAllAsync(n => n.PartyRoom, m => m.PartyTheme, o => o.PartyOrganizator );

            return View(allParties);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allParties = await _service.GetAllAsync(n => n.PartyRoom, m => m.PartyTheme, o => o.PartyOrganizator);
            
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResultNew = allParties.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allParties);
        }

        //GET: Parties/Details/1

        public async Task<IActionResult> Details(int id)
        { 
            var partyDetail = await _service.GetPartyByIdAsync(id);
          
            return View(partyDetail);
        }
        //GET: Parties/Create
        public async Task<IActionResult> Create()
        {
            var partyDropdownData = await _service.GetNewPartyDropdownsValues();

            ViewBag.RoomId = new SelectList(partyDropdownData.Rooms, "Id", "Name");
            ViewBag.OrgId = new SelectList(partyDropdownData.Organizators, "Id", "FirstName");
            ViewBag.ThemeId = new SelectList(partyDropdownData.Themes, "Id", "ThemeName");
            ViewBag.FeatureIds = new SelectList(partyDropdownData.Features, "Id", "Description");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewPartyVM party)
        {
            if (!ModelState.IsValid)
            {
                var partyDropdownData = await _service.GetNewPartyDropdownsValues();

                ViewBag.RoomId = new SelectList(partyDropdownData.Rooms, "Id", "Name");
                ViewBag.OrgId = new SelectList(partyDropdownData.Organizators, "Id", "FirstName");
                ViewBag.ThemeId = new SelectList(partyDropdownData.Themes, "Id", "ThemeName");
                ViewBag.FeatureIds = new SelectList(partyDropdownData.Features, "Id", "Description");
                return View(party);
            }

            await _service.AddNewPartyAsync(party);
            return RedirectToAction(nameof(Index));
        }


        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var partyDetails = await _service.GetPartyByIdAsync(id);
            if (partyDetails == null) return View("NotFound");

            var response = new NewPartyVM()
            {
                Id = partyDetails.Id,
                Name = partyDetails.Name,
                Guests = partyDetails.Guests,
                Description = partyDetails.Description,
                Price = partyDetails.Price,
                StartDate = partyDetails.StartDate,
                EndDate = partyDetails.EndDate,
                Picture = partyDetails.Picture,
                AdditionalCost = (Data.Enums.AdditionalCost)partyDetails.AdditionalDecorationsCost,
                RoomId = partyDetails.RoomId,
                OrgId = partyDetails.OrgId,
                FeatureIds = partyDetails.Party_Feature.Select(n => n.FeatureId).ToList(),
            };

            var partyDropdownData = await _service.GetNewPartyDropdownsValues();

            ViewBag.RoomId = new SelectList(partyDropdownData.Rooms, "Id", "Name");
            ViewBag.OrgId = new SelectList(partyDropdownData.Organizators, "Id", "FirstName");
            ViewBag.ThemeId = new SelectList(partyDropdownData.Themes, "Id", "ThemeName");
            ViewBag.FeatureIds = new SelectList(partyDropdownData.Features, "Id", "Description");
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewPartyVM party)
        {
            if (id != party.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var partyDropdownData = await _service.GetNewPartyDropdownsValues();

                ViewBag.RoomId = new SelectList(partyDropdownData.Rooms, "Id", "Name");
                ViewBag.OrgId = new SelectList(partyDropdownData.Organizators, "Id", "FirstName");
                ViewBag.ThemeId = new SelectList(partyDropdownData.Themes, "Id", "ThemeName");
                ViewBag.FeatureIds = new SelectList(partyDropdownData.Features, "Id", "Description");

                return View(party);
            }

            await _service.UpdatePartyAsync(party);
            return RedirectToAction(nameof(Index));
        }

       // [HttpPost]
       // public ActionResult Calculate(NewPartyVM party)
       // {
       //    ViewBag.Price = _service.CalculateCost(party);
        //   return View(ViewBag.Price);
       // }

       
        public IActionResult Success()
        {
            // var allParties = await _service.GetAllAsync(n => n.PartyRoom, m => m.PartyTheme, o => o.PartyOrganizator);
            //  var names = allParties.Select(i => i.StartDate).ToList();
            //var datesonly = names.Select(StartDate => StartDate.Date).ToArray();

            //  ViewBag.Dates = datesonly.ElementAt(0);
            // return View(ViewBag.Dates);
            // return Json(datesonly, new Newtonsoft.Json.JsonSerializerSettings());
            return View();
           
        }
    }
}
