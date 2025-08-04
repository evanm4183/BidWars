using BidWars.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidWars.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LocationsController : Controller
    {
        private readonly BidWarsContext _context;

        public LocationsController(BidWarsContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations.ToListAsync();
                
            return View(locations);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Locations.Add(location);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
