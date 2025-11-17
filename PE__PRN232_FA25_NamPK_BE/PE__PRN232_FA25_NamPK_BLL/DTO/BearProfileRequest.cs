using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE__PRN232_FA25_NamPK_BLL.DTO
{
    public class BearProfileRequest
    {
        public int? BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public decimal? BearWeight { get; set; }

        public string? Characteristics { get; set; }

        public int? CareNeeds { get; set; }

        public DateOnly? ModifiedDate { get; set; }
    }
}
