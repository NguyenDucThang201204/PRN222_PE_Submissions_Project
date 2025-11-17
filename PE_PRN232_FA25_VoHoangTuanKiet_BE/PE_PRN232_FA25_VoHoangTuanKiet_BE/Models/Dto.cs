using System;
using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_VoHoangTuanKiet_BE.Models
{
    public class Dto
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        [RegularExpression(@"^[A-Z]*[A-Za-z0-9]*$", ErrorMessage = "Name must match pattern!")]
        [Length(4, 50)]
        public string BearName { get; set; } = null!;

        [Range(200, double.MaxValue, ErrorMessage = "Number should be positive")]
        public decimal BearWeight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
