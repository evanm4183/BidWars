using BidWars.Database;
using BidWars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BidWars.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        private readonly BidWarsContext _context;

        public ProductsController(BidWarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.ProductCategory)
                .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var productCategories = await _context.ProductCategories.ToListAsync();
            var model = new ProductVM { ProductCategories = productCategories };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductVM model)
        {
            if (!ModelState.IsValid)
            {
                model.ProductCategories = await _context.ProductCategories.ToListAsync();

                return View(model);
            }

            var product = new Product()
            {
                ProductName = model.ProductName,
                Description = model.Description,
                ProductCategoryId = model.ProductCategoryId.Value
            };
            _context.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
