using System;
using System.Collections.Generic;

namespace BO;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int? BearTypeId { get; set; }

    public string BearName { get; set; } = null!;

    public double BeartWeight { get; set; }

    public string Characteristics { get; set; } = null!;

    public string CareNeeds { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
