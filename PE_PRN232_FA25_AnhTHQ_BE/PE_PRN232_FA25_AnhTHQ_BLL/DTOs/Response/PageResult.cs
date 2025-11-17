using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response
{
    public class PagedResult<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public List<T> Items { get; set; } = new();
    }
}
