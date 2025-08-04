using BidWars.Database;
using System.ComponentModel.DataAnnotations;

namespace BidWars.Models
{
    public class AuctionVM
    {
        [Required]
        [Display(Name = "Product")]
        public int? ProductId { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        [Required]
        [Display(Name = "Starting Amount")]
        public decimal? StartingAmount { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        public List<Product>? Products { get; set; }

        public List<Location>? Locations { get; set; }
    }
}
