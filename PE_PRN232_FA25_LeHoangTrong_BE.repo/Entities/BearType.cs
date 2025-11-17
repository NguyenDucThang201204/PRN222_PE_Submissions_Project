using System;
using System.Collections.Generic;

namespace PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

public partial class BearType
{
    public int BearTypeId { get; set; }

    public string BearTypeName { get; set; } = null!;

    public string Origin { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
