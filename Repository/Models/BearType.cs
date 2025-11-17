using System;
using System.Collections.Generic;

namespace Repository.Models;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; } = null!;

    public string? Origin { get; set; }

    public int? Description { get; set; }

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
