using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public enum ErrorCode
    {
        HB40001,  //Missinginvalid input
        HB40101,  //Token missinginvalid
        HB40301,  //Permission denied
        HB40401,  //Resource not found
        HB50001   //Internal server error
    }
}
