using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Repositories.ModelExtensions
{
    public class BearProfileDTO
    {
        [Required]
        public int BearProfileId { get; set; }
        [Required]
        public int? BearTypeId { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z][a-zA-Z]*\s)*([A-Z][a-zA-Z]*)$",
       ErrorMessage = "Name must start with an uppercase letter or digit.")]
        public string BearName { get; set; }
        [Required]
        public string Characteristics { get; set; }
        [Required]
        [Range(200, int.MaxValue, ErrorMessage = "Weight must be greater than 200.")]
        public decimal? BearWeight { get; set; }
        [Required]
        public int? CareNeeds { get; set; }
        [Required]
        public DateOnly? ModifiedDate { get; set; }
    }
}
