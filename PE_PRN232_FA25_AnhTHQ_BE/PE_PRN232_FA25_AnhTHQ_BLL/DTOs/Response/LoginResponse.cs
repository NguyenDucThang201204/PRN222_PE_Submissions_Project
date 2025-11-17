using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response
{
    public class LoginResponse
    {
        public String token { get; set; }
        public int role { get; set; }
    }
}
