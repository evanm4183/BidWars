using BidWars.Database;
using System.ComponentModel.DataAnnotations;

namespace BidWars.Models
{
    public class OpenAuctionVM
    {
        public int AuctionId { get; set; }

        public Product Product { get; set; }

        [Display(Name = "Top Bid")]
        public decimal TopBid { get; set; }

        public Location Location { get; set; }

        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }
    }
}
