using System;
using System.Collections.Generic;

namespace BidWars.Database;

public partial class Product
{
    public int Id { get; set; }

    public int ProductCategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual ProductCategory ProductCategory { get; set; } = null!;
}
