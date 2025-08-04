using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BidWars.Database;

public partial class Location
{
    public int Id { get; set; }

    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    [StringLength(2)]
    public string State { get; set; } = null!;

    [StringLength(5)]
    public string Zip { get; set; } = null!;

    [Display(Name = "Location Name")]
    public string LocationName { get; set; } = null!;

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();
}
