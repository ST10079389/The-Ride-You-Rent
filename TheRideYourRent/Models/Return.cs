using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TheRideYourRent.Models;

public partial class Return
{
    public int ReturnId { get; set; }

    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? ReturnDate { get; set; }

    public int? ElapsedDate { get; set; }

    [ReadOnly(true)]
    public int? Fine { get; set; }

    public string? CarNo { get; set; }

    public string? InspectorNo { get; set; }

    public int? DriverId { get; set; }

    public int? MakeId { get; set; }

    public virtual Car? CarNoNavigation { get; set; }

    public virtual Driver? Driver { get; set; }

    public virtual Inspector? InspectorNoNavigation { get; set; }

    public virtual CarMake? Make { get; set; }
}
