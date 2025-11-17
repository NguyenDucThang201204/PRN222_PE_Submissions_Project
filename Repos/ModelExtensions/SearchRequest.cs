using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }

        public int? PageSize { get; set; } = 10;
    }

    public class MainSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }

        public decimal? BearWeight { get; set; }

        public string? BearTypeName { get; set; }
    }
}
