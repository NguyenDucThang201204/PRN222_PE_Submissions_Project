using System;
using System.Collections.Generic;

namespace BOs;

public partial class BearType
{
    public string BearTypeId { get; set; } = null!;

    public string? BearTypeName { get; set; }

    public string? Origin { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
