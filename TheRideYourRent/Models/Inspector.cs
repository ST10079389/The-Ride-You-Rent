using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class Inspector
{
    public int InspectorId { get; set; }

    public string InspectorNo { get; set; } = null!;

    public string? Name { get; set; }

    public string? Email { get; set; }

    public int? Mobile { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
}
