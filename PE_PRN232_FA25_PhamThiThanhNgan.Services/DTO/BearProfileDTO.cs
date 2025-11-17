using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice_FA25_PE_Services.DTO
{
    public class BearProfileDTO
    {
        public int BearProfileId { get; set; }
        public int BearTypeId { get; set; }
        public string BearName { get; set; } = null!;
        public double Weight { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? BearTypeName { get; set; }
        public string Characteristics { get; set; } = null!;
        public string CareNeeds { get; set; } = null!;
    }
}
