using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PE_PRN232_FA25_LeCongHung_DAL.Entities
{
    [Table("BearAccount")]
    public class BearAccount
    {
        [Key]
        public int AccountId { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = null!;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = null!;

        [MaxLength(100)]
        public string? FullName { get; set; }

        [MaxLength(100)]
        public string? Email { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        public int? RoleId { get; set; }
    }
}

