using System;
using System.Collections.Generic;

namespace BO;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int? BearTypeId { get; set; }

    public string BearName { get; set; } = null!;

    public string? Characteristics { get; set; }

    public string? CareNeeds { get; set; }

    public decimal? BearWeight { get; set; }

    public int? Stock { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
