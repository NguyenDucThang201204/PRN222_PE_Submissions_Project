using System;
using System.Collections.Generic;

namespace FA25Bear.DataAccess.Models;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int? BearTypeId { get; set; }

    public string BearName { get; set; } = null!;

    public int? BearWeight { get; set; }

    public string? Characteristics { get; set; }

    public string? CareNeeds { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
