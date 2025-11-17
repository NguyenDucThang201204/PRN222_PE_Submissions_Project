using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class BearProfileCreateDTO
    {
        [Required]
        public int BearTypeId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string BearName { get; set; } = null!;

        [Required]
        public decimal BearWeight { get; set; }

        [Required]
        public string Characteristics { get; set; } = null!;

        [Required]
        public string CareNeeds { get; set; } = null!;
    }
}

