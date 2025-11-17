using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repo.DTO
{
    public class CreateRequest
    {
        
       
        [Required]
        public int? BearTypeId { get; set; }
        [Required]
        [RegularExpression(@"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$")]
        
        public string BearName { get; set; }
        [Required]
        [Range(200.00001, double.MaxValue, ErrorMessage = "BearWeight must be greater than 200")]
        public int BearWeight { get; set; }
        [Required]
        public string Characteristics { get; set; }
        [Required]
        public string CareNeeds { get; set; }
        [Required]
        public DateOnly? ModifiedDate { get; set; }
      
    }
}
