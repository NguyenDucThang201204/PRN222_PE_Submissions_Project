using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public partial class BearProfile
    {
        public int BearProfileId { get; set; }
        public int BearTypeId { get; set; }
        public string BearName { get; set; }
        public int BearWeight { get; set; }
        public string Characteristics { get; set; }
        public string CareNeeds { get; set; }
        public DateOnly ModifiedDate { get; set; }

        public virtual BearType? BearType { get; set; }
    }
}
