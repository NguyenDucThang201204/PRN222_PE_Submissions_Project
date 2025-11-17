using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public partial class BearType
    {
        public int BearTypeId { get; set; }
        public string BearTypeName { get; set; }
        public string Origin { get; set; }
        public string Description { get; set; }
        public virtual ICollection<BearProfile>? BearProfiles { get; set; }
    }
}
