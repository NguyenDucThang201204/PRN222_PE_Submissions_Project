using System;
using System.Collections.Generic;

namespace PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    public int BearTypeId { get; set; }

    public string? BearName { get; set; }

    public int? BearWeight { get; set; }

    public string? Characteristics { get; set; }

    public bool? CareNeeds { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual BearType BearType { get; set; } = null!;
}
