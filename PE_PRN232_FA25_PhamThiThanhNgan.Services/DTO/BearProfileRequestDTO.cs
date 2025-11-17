using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_FA25_PE_Services.DTO
{
    public class BearProfileRequestDTO
    {
        [Required(ErrorMessage = "Bear Type ID is required.")]
        public int BearTypeId { get; set; }

        [Required(ErrorMessage = "Bear Name is required.")]
        [StringLength(50, ErrorMessage = "Bear Name must be less than 50 characters.")]
        [RegularExpression(
            @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$",
            ErrorMessage = "Bear Name must start each word with an uppercase letter/number, cannot have leading/trailing spaces, and only allows letters, numbers, and '#'.")]
        public string BearName { get; set; } = null!;

        [Required(ErrorMessage = "Weight is required.")]
        
        [Range(200.01, 1000.0, ErrorMessage = "Weight must be greater than 200.")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Characteristics is required.")]
        [StringLength(2000)]
        public string Characteristics { get; set; } = null!;

        [Required(ErrorMessage = "Care Needs is required.")]
        [StringLength(1500)]
        public string CareNeeds { get; set; } = null!;
    }
}
