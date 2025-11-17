using System.ComponentModel.DataAnnotations;

namespace Repository.Models
{
    public class BearType
    {
        [Key]
        public int BearTypeId { get; set; }
        public string BearTypeName { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
