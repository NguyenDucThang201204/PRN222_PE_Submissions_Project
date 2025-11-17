using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }
    }

    public class BearProfileRequest : SearchRequest
    {
        public string? BearName { get; set; }
        public double? BearWeight { get; set; }
        //public string? BearTypeName { get; set; }
    }
}
