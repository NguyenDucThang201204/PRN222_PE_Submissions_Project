using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Request
{
    public class CreateProfileRequest
    {
        [Required]
        public int BearProfileId { get; set; }

        [Required]
        public int BearTypeId { get; set; }

        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9]*\s)*([A-Z0-9][a-zA-Z0-9]*)$", ErrorMessage = "ModelName must start with a letter or number and contain only letters, numbers, and spaces.")]
        [Required]
        [Length(4, 50, ErrorMessage = "BearName must be between 4 and 50 characters.")]
        public string BearName { get; set; }

        [Required]
        [Range(200, double.MaxValue, ErrorMessage = "Weight must be greater than 200.")]
        public double BearWeight { get; set; }

        [Required]
        public string Characteristics { get; set; }

        [Required]
        public string CareNeeds { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
