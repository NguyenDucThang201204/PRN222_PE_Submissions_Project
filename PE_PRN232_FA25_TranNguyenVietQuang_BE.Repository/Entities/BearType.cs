using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

public partial class BearType
{
    [Key]
    public string BearTypeId { get; set; } = null!;

    public string? BearTypeName { get; set; }

    public string? Origin { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
}
