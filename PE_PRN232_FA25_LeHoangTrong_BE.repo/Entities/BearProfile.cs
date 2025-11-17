using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;

public partial class BearProfile
{
    public int BearProfileId { get; set; }

    [Required(ErrorMessage = "BearTypeId is required")]
    public int? BearTypeId { get; set; }

    [Required(ErrorMessage = "BearName is required")]
    [Range(4, 50, ErrorMessage = "BearName must be between 4 and 50 characters")]
    [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$",
ErrorMessage = "BearName must start with uppercase letter or number, and can contain letters, numbers, # and spaces")]
    public string? BearName { get; set; }

    [Required(ErrorMessage = "BearWeight is required")]
    [Range(200, double.MaxValue, ErrorMessage = "BearWeight must be between 0.1 and 1000.0")]
    public decimal? BearWeight { get; set; }

    [Required(ErrorMessage = "Characteristics is required")]
    public string? Characteristics { get; set; }

    [Required(ErrorMessage = "CareNeeds is required")]
    public string? CareNeeds { get; set; }

    [Required(ErrorMessage = "ModifiedDate is required")]
    public DateTime? ModifiedDate { get; set; }

    public virtual BearType? BearType { get; set; }
}
