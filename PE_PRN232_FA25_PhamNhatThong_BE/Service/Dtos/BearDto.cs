using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Service.Dtos
{
    public class CreateDto
    {
        [Required]
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$")]
        [DefaultValue("Default Value")]
        [Length(4, 50)]
        public string BearName { get; set; } = null!;

        [Required]
        [Range(201, double.MaxValue)]
        public double BearWeight { get; set; }

        [Required]
        public string Characteristics { get; set; } = null!;

        [Required]
        public string CareNeeds { get; set; } = null!;

        [Required]
        public int BearTypeId { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

    }
    public class UpdateDto : CreateDto
    {
        
    }

    public class ResponseDto
    {
        public int BearProfileId { get; set; }

        public int BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public double BearWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }


        public string BearTypeName { get; set; } = null!;
    }

    public class SearchGroup
    {
        public string BearTypeName { get; set; } = null!;
        public List<ResponseDto> Items { get; set; } = new();
    }
}
