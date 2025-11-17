using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int? BearTypeId { get; set; }

    public string? BearName { get; set; }

    public string? BearWeight { get; set; }

    public int? Characteristics { get; set; }

    public double? CareNeeds { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
