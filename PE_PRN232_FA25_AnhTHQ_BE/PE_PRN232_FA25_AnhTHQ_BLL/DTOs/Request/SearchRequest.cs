using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Request
{
    public class SearchRequest
    {
        public int currentPage { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public String bearName { get; set; } = "";
        public int bearWeight { get; set; } = 0;
        public String bearTypeName { get; set; } = "";
    }
}
