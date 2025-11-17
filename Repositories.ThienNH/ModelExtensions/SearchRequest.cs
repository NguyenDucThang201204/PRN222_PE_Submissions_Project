using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ThienNH.ModelExtensions
{
    public class SearchRequest
    {
        public int? currentPage { get; set; }
        public int? pageSize { get; set; }
    }

    public class BearProfileSearchRequest : SearchRequest
    {
        public string BearName { get; set; }

        public double Weight { get; set; }

        public string BearTypeName { get; set; }
    }
}
