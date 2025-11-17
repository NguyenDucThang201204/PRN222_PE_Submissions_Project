namespace Data.Entities
{
    public class BearProfile
    {
        public int BearProfileId { get; set; }
        public int BearTypeId { get; set; }
        public string BearName { get; set; }
        public int BearWeight { get; set; }
        public string? Characteristics { get; set; }
        public string? CareNeeds { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
