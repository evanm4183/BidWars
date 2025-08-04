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

    // May not be a good idea to put this logic in the entity class, but I'm doing it here
    // for the sake of brevity.
    public decimal GetTopBid()
    {
        if (Bids == null || Bids.Count == 0)
        {
            return StartingAmount;
        }
        else
        {
            return Bids.Max(b => b.BidAmount);
        }
    }
}
