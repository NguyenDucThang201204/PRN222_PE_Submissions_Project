

namespace Service.Model
{
    public class BearCreateDTO
    {
        public int BearTypeId { get; set; }

        public string BearName { get; set; } = null!;
       
        public double Weight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

    }
}
