using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class CarBodyType
{
    public int BodyTypeId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Car> Cars { get; set; } = new List<Car>();
}
