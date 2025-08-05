using System.ComponentModel.DataAnnotations;

namespace BidWars.Models
{
    public class PlaceBidVM
    {
        public OpenAuctionVM Auction { get; set; }

        [Display(Name = "Bid Amount")]
        [Required]
        public decimal? BidAmount { get; set; }
    }
}
