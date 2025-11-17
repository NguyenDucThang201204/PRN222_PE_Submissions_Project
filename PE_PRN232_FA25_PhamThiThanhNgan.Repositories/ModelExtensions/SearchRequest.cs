using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

    }
    public class BearSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }
        public double? BearWeigth { get; set; }
        public string? BearTypeName { get; set; }
    }
}
