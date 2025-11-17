using System.ComponentModel.DataAnnotations;

namespace Repositories.ModelExtensions
{
    public class UpdateRequest
    {
        [Required(ErrorMessage = "BearName is required")]
        public string BearName { get; set; }

        [Required(ErrorMessage = "BearWeight is required")]
        [Range(15, double.MaxValue, ErrorMessage = "BearWeight must be greater than 15")]
        public double BearWeight { get; set; }

        [Required(ErrorMessage = "Length is required")]
        public string Characteristics { get; set; }

        [Required(ErrorMessage = "CareNeeds is required")]
        public string CareNeeds { get; set; }

        [Required(ErrorMessage = "ModifiedDate is required")]
        public DateTime ModifiedDate { get; set; }

        [Required(ErrorMessage = "BearTypeId is required")]
        public int BearTypeId { get; set; }
    }
}
