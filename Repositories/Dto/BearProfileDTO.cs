using Repositories.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Dto
{
    public class BearProfileDTO
    {
        public int BearProfileId { get; set; }

        public string BearName { get; set; } = null!;

        public int BearWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

        public string BearType { get; set; } = null!;
    }

    public class CreateBearProfileDTO
    {
        [Required]
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$",
        ErrorMessage = "Model Name must start with uppercase letter or digit")]
        [Range(4, 50)]
        public string BearName { get; set; } = null!;

        [Required]
        [Range(200, int.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public int BearWeight { get; set; }

        [Required]
        public string Characteristics { get; set; } = null!;

        [Required]
        public string CareNeeds { get; set; } = null!;

        [Required]
        public DateTime ModifiedDate { get; set; }

        [Required]
        public int? BearTypeId { get; set; } = null!;
    }
}
