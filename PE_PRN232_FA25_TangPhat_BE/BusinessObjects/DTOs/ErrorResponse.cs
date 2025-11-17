using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public static ErrorResponse Fail(ErrorCode code, string message)
        {
            return new ErrorResponse
            {
                ErrorCode = code.ToString(),
                Message = message
            };
        }
    }
}
