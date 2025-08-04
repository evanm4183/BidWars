using BidWars.Database;
using BidWars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BidWars.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AuctionsController : Controller
    {
        private readonly BidWarsContext _context;

        public AuctionsController(BidWarsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Include(a => a.Location)
                .OrderByDescending(a => a.EndTime)
                .ToListAsync();

            return View(auctions);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Would try to execute these queries concurrently, but multithreading isn't possible
            // for the same DbContext instance.
            var products = await _context.Products.ToListAsync();
            var locations = await _context.Locations.ToListAsync();

            var model = new AuctionVM()
            {
                Products = products,
                Locations = locations,
                
                // In a real application, you would want to carefully consider how you store/display times.
                // I would probably store it in the database as UTC time, then convert that time to the user's
                // timezone in the UI. However, for the sake of simplicity and expediency, I'm going to assume 
                // ther is only one timezone: CST.
                StartTime = DateTime.Now,
                EndTime = DateTime.Now
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuctionVM model)
        {
            if (!ModelState.IsValid)
            {
                model.Products = await _context.Products.ToListAsync();
                model.Locations = await _context.Locations.ToListAsync();

                return View(model);
            }

            var auction = new Auction()
            {
                ProductId = model.ProductId.Value,
                LocationId = model.LocationId.Value,
                StartingAmount = model.StartingAmount.Value,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
            };
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
