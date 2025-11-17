using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_NguyenQuangMinh_BE.Repo.ModelExtensions
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }

        public int? PageSize { get; set; }
    }

    public class BearSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }
        public string? BearWeight { get; set; }
        public string? BearTypeName { get; set; }
    }
}
