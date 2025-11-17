using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }

    public class SearchDataRequest : SearchRequest
    {
        public double? Weight { get; set; }
        public string? Characteristics { get; set; }
        public string? CareNeeds { get; set; }
    }
}
