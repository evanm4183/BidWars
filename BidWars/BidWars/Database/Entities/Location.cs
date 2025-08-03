using System;
using System.Collections.Generic;

namespace BidWars.Database;

public partial class Location
{
    public int Id { get; set; }

    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Zip { get; set; } = null!;

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
