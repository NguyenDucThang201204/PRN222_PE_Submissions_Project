using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    public class BearProfile
    {
        [Key]
        public int BearProfileId { get; set; }

        public string? BearName { get; set; } = string.Empty;
        public double? BearWeight { get; set; }
        public string? Characteristics { get; set; } = string.Empty;
        public string? CareNeeds { get; set; } = string.Empty;
        public DateTime ModifiedDate { get; set; }

        [ForeignKey("BearType")]
        public int BearTypeId { get; set; }
        public BearType BearType { get; set; }
    }
}
