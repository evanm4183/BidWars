using BidWars.Database;
using System.ComponentModel.DataAnnotations;

namespace BidWars.Models
{
    public class ProductVM
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int? ProductCategoryId { get; set; }

        public List<ProductCategory>? ProductCategories {get; set;}
    }
}
