namespace FA25Bear.DataAccess.Models.DTOs
{
    public class BearProfileDTO
    {
        public int BearProfileId { get; set; }

        public int? BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public int? BearWeight { get; set; }

        public string? Characteristics { get; set; }

        public string? CareNeeds { get; set; }

        public DateOnly? ModifiedDate { get; set; }

    }
}
