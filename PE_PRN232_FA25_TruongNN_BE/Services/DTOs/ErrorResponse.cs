using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs
{
    public record ErrorResponse(string ErrorCode, string Message)
    {
        public static ErrorResponse HB40001(string message) => new("HB40001", message);
        public static ErrorResponse HB40101(string message) => new("HB40101", message);
        public static ErrorResponse HB40301(string message) => new("HB40301", message);
        public static ErrorResponse HB40401(string message) => new("HB40401", message);
        public static ErrorResponse HB50001(string message) => new("HB50001", message);
    }

}
