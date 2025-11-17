using System;
using System.Collections.Generic;

namespace BO;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string? BearTypeName { get; set; }

    public string? Description { get; set; }

    public string? Origin { get; set; }

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
