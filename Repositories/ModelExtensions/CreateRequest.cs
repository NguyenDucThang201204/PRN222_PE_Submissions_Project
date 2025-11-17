using System.ComponentModel.DataAnnotations;

namespace Repositories.ModelExtensions
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "BearProfileId is required")]
        public int BearProfileId { get; set; }

        [Required(ErrorMessage = "BearTypeId is required")]
        public int BearTypeId { get; set; }

        [Required(ErrorMessage = "BearName is required")]
        //[RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]\s)([A-Z0-9][a-zA-Z0-9#]*)$", ErrorMessage = "BearName is invalid format")]
        [RegularExpression(@"^[A-Z][a-zA-Z0-9#\s]{3,49}$", ErrorMessage = "BearName must start with an uppercase letter and be 4-50 characters long")]
        [Length(4, 50, ErrorMessage = "BearName cannot exceed 50 characters")]
        public string BearName { get; set; }

        [Required(ErrorMessage = "BearWeight is required")]
        [Range(200, double.MaxValue, ErrorMessage = "BearWeight must be greater than 200")]
        public double BearWeight { get; set; }

        [Required(ErrorMessage = "Length is required")]
        public string Characteristics { get; set; }

        [Required(ErrorMessage = "CareNeeds is required")]
        public string CareNeeds { get; set; }

        [Required(ErrorMessage = "ModifiedDate is required")]
        public DateTime ModifiedDate { get; set; }

    }
}
