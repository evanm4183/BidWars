using System;
using System.Collections.Generic;

namespace BidWars.Database;

public partial class Auction
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int LocationId { get; set; }

    public decimal StartingAmount { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual Location Location { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
