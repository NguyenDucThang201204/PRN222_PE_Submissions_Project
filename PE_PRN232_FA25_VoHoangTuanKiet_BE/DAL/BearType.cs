using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DAL;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; } = null!;

    public string? Origin { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
