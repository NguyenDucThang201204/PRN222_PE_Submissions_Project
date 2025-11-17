using System;
using System.Collections.Generic;

namespace PE__PRN232_FA25_NamPK_DAL.Models;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int? BearTypeId { get; set; }

    public string BearName { get; set; } = null!;

    public decimal? BearWeight { get; set; }

    public string? Characteristics { get; set; }

    public int? CareNeeds { get; set; }

    public DateOnly? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
