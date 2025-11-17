using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.Dto
{
    public class BearDto
    {

        public int? BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public double BeartWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }

    }

    public class BearDtoResponse
    {

        public int? BearTypeId { get; set; }

        public string BearName { get; set; } = null!;

        public double BeartWeight { get; set; }

        public string Characteristics { get; set; } = null!;

        public string CareNeeds { get; set; } = null!;

        public DateTime ModifiedDate { get; set; }


        public string BearTypeName { get; set; } = string.Empty;
    }
}
