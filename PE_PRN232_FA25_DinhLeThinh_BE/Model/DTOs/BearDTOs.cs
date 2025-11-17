using System.ComponentModel.DataAnnotations;

namespace Model.DTOs
{
    public class BearDto
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        public string? BearTypeName { get; set; }

        public string BearName { get; set; } = null!;

        public double BearWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }
    }

    public class BearReq
    {
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$", ErrorMessage = "BearName wrong format.")]
        [Required(ErrorMessage = "BearName is required.")]
        public string BearName { get; set; } = null!;

        [Range(200, double.MaxValue, ErrorMessage = "Weight must be greater than 200")]
        [Required(ErrorMessage = "BearWeight is required.")]
        public double BearWeight { get; set; }

        [Required(ErrorMessage = "Characteristics is required.")]
        public string Characteristics { get; set; } = null!;

        [Required(ErrorMessage = "CareNeeds is required.")]
        public string CareNeeds { get; set; } = null!;
    }
}
