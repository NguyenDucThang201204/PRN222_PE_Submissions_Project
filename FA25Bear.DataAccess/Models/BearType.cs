using System;
using System.Collections.Generic;

namespace FA25Bear.DataAccess.Models;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; } = null!;

    public string? Origin { get; set; }

    public string? Descriptionn { get; set; }

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
