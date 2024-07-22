using System;
using System.Collections.Generic;

namespace TheRideYourRent.Models;

public partial class InspectorRegister
{
    public int InspectorId { get; set; }

    public string? Name { get; set; }

    public string? Password { get; set; }
}
