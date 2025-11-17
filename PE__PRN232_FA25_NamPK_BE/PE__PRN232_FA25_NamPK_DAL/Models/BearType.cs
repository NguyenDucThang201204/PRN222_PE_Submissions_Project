using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PE__PRN232_FA25_NamPK_DAL.Models;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; } = null!;

    public string? Origin { get; set; }

    public string? Description { get; set; }

    [JsonIgnore]
    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
