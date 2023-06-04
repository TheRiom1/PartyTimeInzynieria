using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Data.Services;
using WebApplication3.Data.Static;
using WebApplication3.Models;
using X.PagedList;
using X.PagedList.Mvc;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {
        
            private readonly IProductsService _service;

            public ProductsController(IProductsService service)
            {
                _service = service;
            }

            [AllowAnonymous]
            public async Task<IActionResult> Index(int? page)
            {
            int pageNumber = page ?? 1;
            int pageSize = 3;
                var allProducts = await _service.GetAllAsync( pageNumber, pageSize, n => n.ProductTheme);
                return View(allProducts);
            }

            [AllowAnonymous]
            public async Task<IActionResult> Filter(string searchString, int? page)
            {
            int pageNumber = page ?? 1;
            int pageSize = 3;
            var allProducts = await _service.GetAllAsync(pageNumber, pageSize, n => n.ProductTheme);

            if (!string.IsNullOrEmpty(searchString))
                {
                    //var filteredResult = allProducts.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                    var filteredResultNew = allProducts.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToPagedList();

                    return View("Index", filteredResultNew);
                }

                return View("Index", allProducts);
            }

            //GET: Products/Details/1
            [AllowAnonymous]
            public async Task<IActionResult> Details(int id)
            {
                var ProductDetail = await _service.GetProductByIdAsync(id);
                return View(ProductDetail);
            }

            //GET: Products/Create
            public async Task<IActionResult> Create()
            {
                var ProductDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.ThemeId = new SelectList(ProductDropdownsData.Themes, "Id", "ThemeName");
 

                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(NewProductVM Product)
            {
                if (!ModelState.IsValid)
                {
                    var ProductDropdownsData = await _service.GetNewProductDropdownsValues();

                    ViewBag.Themes = new SelectList(ProductDropdownsData.Themes, "Id", "ThemeName");


                    return View(Product);
                }

                await _service.AddNewProductAsync(Product);
                return RedirectToAction(nameof(Index));
            }


            //GET: Products/Edit/1
            public async Task<IActionResult> Edit(int id)
            {
                var ProductDetails = await _service.GetProductByIdAsync(id);
                if (ProductDetails == null) return View("NotFound");

                var response = new NewProductVM()
                {
                    Id = ProductDetails.Id,
                    Name = ProductDetails.Name,
                    Description = ProductDetails.Description,
                    Price = ProductDetails.Price,
                    Picture = ProductDetails.Picture,
                    ProductCategory = ProductDetails.ProductCategory,
                    ThemeId = ProductDetails.ThemeId,

                };

                var ProductDropdownsData = await _service.GetNewProductDropdownsValues();
                ViewBag.ThemeId = new SelectList(ProductDropdownsData.Themes, "ThemeId", "ThemeName");

                return View(response);
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, NewProductVM Product)
            {
                if (id != Product.Id) return View("NotFound");

                if (!ModelState.IsValid)
                {
                    var ProductDropdownsData = await _service.GetNewProductDropdownsValues();

                    ViewBag.ThemeId = new SelectList(ProductDropdownsData.Themes, "ThemeId", "ThemeName");

                    return View(Product);
                }

                await _service.UpdateProductAsync(Product);
                return RedirectToAction(nameof(Index));
            }
        }
    }

