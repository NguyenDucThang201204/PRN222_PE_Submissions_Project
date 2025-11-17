using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Model
{
    public class ItemEditModel
    {
        public int BearProfileId { get; set; }
        [Required(ErrorMessage = "BearTypeId is required")]
        public int BearTypeId { get; set; }
        [Required(ErrorMessage = "BearName is required")]
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$",
            ErrorMessage = "BearName must start with uppercase letter or number and follow the specified pattern")]
        public string BearName { get; set; } = null!;
        [Required(ErrorMessage = "weight is required")]
        [Range(200.01, double.MaxValue, ErrorMessage = "Bear weight must be greater than 200")]
        public double Weight { get; set; }
        [Required(ErrorMessage = "Characteristics is required")]
        public string Characteristics { get; set; } = null!;
        [Required(ErrorMessage = "CareNeeds is required")]
        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }
    }
}
