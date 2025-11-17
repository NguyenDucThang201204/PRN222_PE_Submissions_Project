using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class GetDTO
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public double BearWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }
        public string? BearTypeName { get; set; }

        public string? Origin { get; set; }

        public string? Description { get; set; }
    }

    public class CreateDTO
    {
        [Required(ErrorMessage = "BearTypeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "BearTypeId not found")]
        public int BearTypeId { get; set; }

        [Required(ErrorMessage = "BearName is required")]
        public string BearName { get; set; } = null!;

        [Required(ErrorMessage = "Weight is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Weight must be greater than 15")]
        public double Weight { get; set; }

        [Required(ErrorMessage = "Characteristics is required")]
        public string Characteristics { get; set; } = null!;

        [Required(ErrorMessage = "CareNeeds is required")]
        public string CareNeeds { get; set; } = null!;

        [Required(ErrorMessage = "ModifiedDate is required")]
        public DateTime ModifiedDate { get; set; }

    }

}
