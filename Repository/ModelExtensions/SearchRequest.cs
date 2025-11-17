using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelExtensions
{
    public class SearchRequest
    {
        public int? currentPage { get; set; } = 1;
        public int? pageSize { get; set; } = 10;
    }
    public class BearProfileSearchRequest : SearchRequest
    {
        public string? typename { get; set; }
        public double? weight { get; set; }
        public string? name { get; set; }
    }
}
