using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension
{
    public class SearchRequest
    {
        public int? CurrentPage { get; set; }
        public int? PageSize { get; set; }

    }

    public class BearProfileSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }

        public int? BearWeight { get; set; }

        public string? BearTypeName { get; set; }
    }
}
