using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ModelExtentions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }

        public int? PageSize { get; set; } = 10;
    }

    public class MainSearchRequest : SearchRequest
    {
        public string? LeopardName { get; set; }

        public double? Weight { get; set; }
    }
}
