using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication3.Data.Base;
using WebApplication3.Data.ViewModels;
using WebApplication3.Models;

namespace WebApplication3.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Picture = data.Picture,
                Description = data.Description,
                Price = data.Price,
                ThemeId = data.ThemeId,
                ProductCategory = data.ProductCategory
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var ProductDetails = await _context.Products
                .Include(c => c.ProductTheme)
                .FirstOrDefaultAsync(n => n.Id == id);

            return ProductDetails;
        }

        public async Task<NewProductDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductDropdownsVM()
            {
                Themes = await _context.Themes.OrderBy(n => n.ThemeName).ToListAsync()

            };

            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.Picture = data.Picture;
                dbProduct.ThemeId = data.ThemeId;
                dbProduct.ProductCategory = data.ProductCategory;
                await _context.SaveChangesAsync();
            }
        }

    }
}
