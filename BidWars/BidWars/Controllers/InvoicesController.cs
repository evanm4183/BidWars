using BidWars.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BidWars.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly BidWarsContext _context;

        public InvoicesController (BidWarsContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Bidder")]
        public async Task<IActionResult> OpenInvoices()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            string userId;
            if (userIdClaim != null)
            {
                userId = userIdClaim.Value;
            }
            else
            {
                return NotFound("User Id was not found.");
            }

            // These joins are simply for "convenience". Wouldn't do it this way in a real app.
            // Explain data model.
            var invoices = await _context.Invoices
                .Include(i => i.Bid)
                    .ThenInclude(b => b.Auction)
                        .ThenInclude(a => a.Product)
                .Include(i => i.Bid)
                    .ThenInclude(b => b.Auction)
                        .ThenInclude(a => a.Location)
                .Where(i => i.UserId == userId && i.PaymentDate == null)
                .ToListAsync();

            return View(invoices);
        }
    }
}
