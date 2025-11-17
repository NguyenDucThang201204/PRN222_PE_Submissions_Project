using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_NguyenThiMaiTrinh_BE
{
    public class ItemEditModel
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$",
    ErrorMessage = "BearName must start with uppercase letter or number and follow the specified pattern")]
        public string BearName { get; set; } = null!;

        public double BearWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }
    }
}
