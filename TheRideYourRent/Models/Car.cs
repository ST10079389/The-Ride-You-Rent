using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class Car
{
    public int CarId { get; set; }

    public string CarNo { get; set; } = null!;

    public string? Model { get; set; }

    public int? KilometresTravelled { get; set; }

    public int? ServiceKilometres { get; set; }

    public string? Available { get; set; }

    public int? MakeId { get; set; }

    public int? BodyTypeId { get; set; }

    public virtual CarBodyType? BodyType { get; set; }

    public virtual CarMake? Make { get; set; }

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();

    public virtual ICollection<Return> Returns { get; set; } = new List<Return>();
}
