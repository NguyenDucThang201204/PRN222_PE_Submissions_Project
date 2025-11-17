using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PE_PRN232_FA25_LeCongHung_DAL.Entities
{
    [Table("BearType")]
    public class BearType
    {
        [Key]
        public int BearTypeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string BearTypeName { get; set; } = null!;

        [MaxLength(100)]
        public string? Origin { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<BearProfile> BearProfiles { get; set; } = new List<BearProfile>();
    }
}

