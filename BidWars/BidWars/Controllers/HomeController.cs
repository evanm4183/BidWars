using System.Diagnostics;
using System.Threading.Tasks;
using BidWars.Database;
using BidWars.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BidWars.Controllers
{
    public class HomeController : Controller
    {
        private readonly BidWarsContext _context;

        public HomeController(BidWarsContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var auctions = await _context.Auctions
                .Include(a => a.Product)
                .Include(a => a.Location)
                .Include(a => a.Bids)
                .Where(a => DateTime.Now < a.EndTime) // Assuming only one time zone
                .ToListAsync();

            // In other circumstances, I would consider using a library for mapping
            // or creating methods for mapping.
            var viewModels = auctions.Select(a => new OpenAuctionVM()
            {
                AuctionId = a.Id,
                Product = a.Product,
                Location = a.Location,
                EndTime = a.EndTime,
                TopBid = a.GetTopBid()
            });

            // Would want to paginate this in a large application
            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
