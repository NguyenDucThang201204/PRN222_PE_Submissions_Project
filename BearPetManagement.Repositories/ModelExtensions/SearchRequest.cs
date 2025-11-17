using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement.Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

    }

    public class SearchTestResult : SearchRequest
    {
        public string? Result { get; set; }
        public decimal? ConfidenceLevel { get; set; }
        public string? FullName { get; set; }

    }
}
