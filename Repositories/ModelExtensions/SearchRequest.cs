using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ModelExtensions
{
    public class SearchRequest
    {
        [Required(ErrorMessage = "Current Page is not null !")]
        [Range(1, int.MaxValue, ErrorMessage = "Current page more than 0")]
        [DefaultValue(2)]
        public int? CurrentPage { get; set; }

        [Required(ErrorMessage = "Page Size is not null !")]
        [Range(1, int.MaxValue, ErrorMessage ="Page size more than 0")]
        [DefaultValue(3)]
        public int? PageSize { get; set; }
    }

    public class MainSearchRequest : SearchRequest
    {
        public string? BearName { get; set; }
        public string? BearTypeName { get; set; }
        public decimal? BearWeight { get; set; }
    }
}
