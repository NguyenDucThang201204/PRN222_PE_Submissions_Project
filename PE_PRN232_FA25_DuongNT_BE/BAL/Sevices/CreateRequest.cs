using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Sevices
{
    public class CreateRequest
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }
        [Required(ErrorMessage = "BearName is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters.")]
        [MaxLength(50, ErrorMessage = "Name can't be greater 50 characters.")]
        [RegularExpression(@"^[A-Z0-9][a-zA-Z0-9#]*(\s[A-Z0-9][a-zA-Z0-9#]*)*$",
            ErrorMessage = "BearName must start with uppercase letter or number")]
        public string BearName { get; set; } = null!;
        [Range(200.1, double.MaxValue, ErrorMessage = "Weight must be greater than 200")]
        public double Weight { get; set; }
        [Required]
        public string Characteristics { get; set; } = null!;
        [Required]
        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }


    }
}
