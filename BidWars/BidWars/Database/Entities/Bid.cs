using System;
using System.Collections.Generic;

namespace BidWars.Database;

public partial class Bid
{
    public int Id { get; set; }

    public int AuctionId { get; set; }

    public string BidderId { get; set; } = null!;

    public DateTime PlacedAt { get; set; }

    public virtual Auction Auction { get; set; } = null!;

    public virtual AspNetUser Bidder { get; set; } = null!;
}
