using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BidWars.Database;

public partial class Product
{
    public int Id { get; set; }

    [Display(Name = "Product Category")]
    public int ProductCategoryId { get; set; }

    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    [Display(Name = "Product Category")]
    public virtual ProductCategory ProductCategory { get; set; } = null!;
}
