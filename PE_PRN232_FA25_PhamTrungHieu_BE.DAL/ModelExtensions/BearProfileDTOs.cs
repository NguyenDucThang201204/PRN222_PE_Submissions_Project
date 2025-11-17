namespace PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions
{
    public class GetBearProfileResponse
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        public string BearName { get; set; }

        public decimal? BearWeight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public GetBearTypeResponse BearType { get; set; }
    }

    public class GetBearTypeResponse
    {
        public int BearTypeId { get; set; }

        public string BearTypeName { get; set; }

        public string Origin { get; set; }

        public string Description { get; set; }
    }

    public class CreateBearProfileRequest
    {
        public int BearTypeId { get; set; }

        public string BearName { get; set; }

        public decimal? BearWeight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }

    public class UpdateBearProfileRequest
    {
        public int BearTypeId { get; set; }

        public string BearName { get; set; }

        public decimal? BearWeight { get; set; }

        public string Characteristics { get; set; }

        public string CareNeeds { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
