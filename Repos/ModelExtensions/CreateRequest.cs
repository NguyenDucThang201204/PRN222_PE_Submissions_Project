using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.ModelExtensions
{
    public class CreateRequest
    {
        [Required(ErrorMessage = "BearProfileId is required")]
        public int BearProfileId { get; set; }

        [Required(ErrorMessage = "BearTypeId is required")]
        public int? BearTypeId { get; set; }

        
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$", ErrorMessage = "Each word must start with a capital letter or digit, and only letters, digits, and # are allowed.")]
        [Required(ErrorMessage = "BearName is required")]
        public string BearName { get; set; }

        [Range(200.01, double.MaxValue, ErrorMessage = "BearWeight must be greater than 200")]
        [Required(ErrorMessage = "BearWeight is required")]
        public decimal? BearWeight { get; set; }
        [Required(ErrorMessage = "Characteristics is required")]
        public string Characteristics { get; set; }
        [Required(ErrorMessage = "CareNeeds is required")]
        public string CareNeeds { get; set; }
        [Required(ErrorMessage = "ModifiedDate is required")]
        public DateOnly? ModifiedDate { get; set; }
    }
}
