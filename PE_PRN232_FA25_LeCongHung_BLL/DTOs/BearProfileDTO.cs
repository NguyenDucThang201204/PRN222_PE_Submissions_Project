namespace PE_PRN232_FA25_LeCongHung_BLL.DTOs
{
    public class BearProfileDTO
    {
        public int BearProfileId { get; set; }
        public int BearTypeId { get; set; }
        public string BearName { get; set; } = null!;
        public decimal? BearWeight { get; set; }
        public string? Characteristics { get; set; }
        public string? CareNeeds { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? BearTypeName { get; set; }
    }
}

