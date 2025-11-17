namespace PE_PRN232_FA25_DaoTrongTien_BE.API.DTOs
{
    public class CreateBearProfileRequest
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }


        public string? BearName { get; set; }

        public int? BearWeight { get; set; }

        public string? Characteristics { get; set; }

        public bool? CareNeeds { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}



