using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs
{
    public enum ApiStatusCode
    {
        HB40001, // Missing/invalid input
        HB40101, // Token missing/invalid
        HB40301, // Permission denied
        HB40401, // Resource not found
        HB50001  // Internal server error
    }
}
