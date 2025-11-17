using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;

public partial class BearProfile
{
    [Key]
    public string BearProfileId { get; set; } = null!;

    [Required]
    public string? BearTypeId { get; set; }

    [Required]
    [RegularExpression(@"^([A-Z\s][a-zA-Z])*([A-Z][a-zA-Z\s]*)$", ErrorMessage = "BearName must start with an uppercase letter and can No special characters (#, @, &, (,)).")]
    public string? BearName { get; set; }

    [Required]
    [Range(200, int.MaxValue, ErrorMessage = "BearWeight must be greater than 200.")]
    public int? BearWeight { get; set; }

    [Required]
    public string? Characteristics { get; set; }

    [Required]
    public string? CareNeeds { get; set; }

    [Required]
    public DateTime? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
