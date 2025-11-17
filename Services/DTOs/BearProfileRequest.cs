using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public class BearProfileRequest
    {

        [Required]
        public int BearTypeId { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$")]
        [MinLength(4)]
        [MaxLength(50)]
        [DefaultValue("Bear Big")]
        public string BearName { get; set; }
        [Required]
        [Range(201, double.MaxValue)]
        public double BearWeight { get; set; }
        [Required]
        public string Characteristics { get; set; }
        [Required]
        public string CareNeeds { get; set; }
        [Required]
        public DateTime ModifiedDate { get; set; }
    }
}
