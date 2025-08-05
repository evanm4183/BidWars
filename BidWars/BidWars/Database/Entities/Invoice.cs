using System;
using System.Collections.Generic;

namespace BidWars.Database;

public partial class Invoice
{
    public int Id { get; set; }

    public string UserId { get; set; } = null!;

    public int BidId { get; set; }

    // This value may be different from the Bid Amount to account for taxes, fees, etc.
    public decimal PaymentAmount { get; set; }

    public DateTime PaymentDueBy { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual Bid Bid { get; set; } = null!;

    public virtual AspNetUser User { get; set; } = null!;
}
