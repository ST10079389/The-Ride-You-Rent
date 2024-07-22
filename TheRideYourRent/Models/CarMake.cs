using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class CarMake
{
    public int MakeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
}
