using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PE_PRN232_FA25_LeCongHung_DAL.Entities
{
    [Table("BearProfile")]
    public class BearProfile
    {
        [Key]
        public int BearProfileId { get; set; }

        [Required]
        public int BearTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BearName { get; set; } = null!;

        [Column(TypeName = "decimal(10,2)")]
        public decimal? BearWeight { get; set; }

        [MaxLength(500)]
        public string? Characteristics { get; set; }

        [MaxLength(500)]
        public string? CareNeeds { get; set; }

        public DateTime? ModifiedDate { get; set; }

        // Navigation property
        [ForeignKey("BearTypeId")]
        public virtual BearType BearType { get; set; } = null!;
    }
}

