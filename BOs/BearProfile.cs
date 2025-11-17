using System;
using System.Collections.Generic;

namespace BOs;

public partial class BearProfile
{
    public string BearProfileId { get; set; } = null!;

    public string? BearTypeId { get; set; }

    public string? BearName { get; set; }

    public double? BearWeight { get; set; }

    public string? Characteristics { get; set; }

    public string? CareNeeds { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
