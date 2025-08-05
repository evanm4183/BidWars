using BidWars.Database;
using BidWars.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BidWars.Controllers
{
    [Authorize(Roles = "Bidder")]
    [Route("{controller}")]
    public class BidsController : Controller
    {
        private readonly BidWarsContext _context;

        public BidsController(BidWarsContext context)
        {
            _context = context;
        }

        [HttpGet("{action}/{auctionId}")]
        public async Task<IActionResult> Create(int auctionId)
        {
            var auction = await _context.Auctions
                .Include(a => a.Product)
                .Include(a => a.Location)
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a =>  a.Id == auctionId);

            if (auction == null)
            {
                return NotFound("Could not find Auction.");
            }
            if (auction.EndTime <= DateTime.Now) // Assuming only one time zone.
            {
                // Would make this "prettier" in a real application.
                return BadRequest("This auction has closed. Bids are no longer being accepted.");
            }

            var model = new PlaceBidVM()
            {
                Auction = new OpenAuctionVM()
                {
                    AuctionId = auctionId,
                    Product = auction.Product,
                    Location = auction.Location,
                    EndTime = auction.EndTime,
                    TopBid = auction.GetTopBid()
                }
            };

            return View(model);
        }

        [HttpPost("{action}/{auctionId}")]
        public async Task<IActionResult> Create(PlaceBidVM model, int auctionId)
        {
            var auction = await _context.Auctions
                .Include(a => a.Bids)
                .FirstOrDefaultAsync(a => a.Id == auctionId);

            if (auction == null)
            {
                return NotFound("Could not find Auction.");
            }

            // Could allow for an offset depending upon how worried we are about a bid potentially
            // being placed after the auction has closed.
            if (auction.EndTime <= DateTime.Now)
            {
                return BadRequest("This bid was placed after the auction closed.");
            }

            // Concurrency considerations here. See more detail below.
            if (model.BidAmount <= auction.GetTopBid())
            {
                return BadRequest("Your bid did not exceed the current highest bid.");
            }

            var bid = new Bid()
            {
                AuctionId = auction.Id,
                BidderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                    ?? throw new Exception("Couldn't find user id when creating Bid"), // Would give more detail in a real app.
                BidAmount = model.BidAmount.Value,
                PlacedAt = DateTime.Now,
            };

            _context.Add(bid);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}

/*
 * Concurrency Considerations when placing bids:
 * 
 * Since, in theory, bids will be being placed in real time by multiple users, we need to consider the concurrency ramifications.
 * One of the business rules is that a bid should not be allowed to be placed unless it exceeds the current top bid. Concurrency
 * has the potential to cause some problems. Assume at t = 0 TopBid = 10. Person A and Person B simultaneously place a bid at t = 0. 
 * Person A's bid is 12 and person B's bid is 11. They both read TopBid at t = 0, so for each person the TopBid = 10. Assume the
 * write operation for Person A completes before the write operation for Person B. Then Person B's bid would violate the business
 * rule.
 * 
 * However, do we care? In some implementations, you certainly would. For example, if you were caching the top bid, you would have to.
 * If not, it could result in the top bid getting decreased. To use the previous example, you could have TopBid = 12 at t = 1, then at 
 * t = 2 it could get set to 11 if you're not careful. However, in my implementation, it shouldn't matter since I'm not caching the 
 * top bid. I'm simply appending the bids to a list. If a lower bid happens to get appended to the list after a higher bid, then it 
 * won't matter because the list is evaluated for the highest bid after the auction closes. The validation is mainly just for the user's
 * convenience in this case.
 * 
 * Another concurrency consideration is what happens if two or more users submit a bid for the same amount at exactly the same time
 * and that bid happens to be the highest bid? This case might be so rare that it's not even worth implementing a solution for, especially
 * if you're tracking time placement down to milliseconds. If not, you would need to come up with some sort of tiebreaker process.
*/
