using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class Driver
{
    public int DriverId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public int? Mobile { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
}
