using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories
{
    public class BearProfileDto
    {
        public int BearProfileId { get; set; }

        [Required]
        public int BearTypeId { get; set; }

        [Required(ErrorMessage = "Bear name is required")]
        [RegularExpression(
            @"^([A-Z0-9][a-zA-Z0-9]*\s)([A-Z0-9][a-zA-Z0-9]*)$",
            ErrorMessage = "Bear name must  each starting with an capital letter, no special characters."
        )]
        public string BearName { get; set; } = null!;

        [Range(200.01, double.MaxValue, ErrorMessage = "Weight must be greater than 200")]
        public double BearWeight { get; set; }

        [Required]
        public string Characteristics { get; set; } = null!;

        [Required]
        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

