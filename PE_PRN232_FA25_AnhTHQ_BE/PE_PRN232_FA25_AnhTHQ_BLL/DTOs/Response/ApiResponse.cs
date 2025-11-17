using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response
{
    public class ApiResponse<T>
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success")
        {
            return new ApiResponse<T>
            {
                StatusCode = "200",
                Message = message,
                Data = data
            };
        }

        // Error response
        public static ApiResponse<T> Error(ApiStatusCode code, string message)
        {
            return new ApiResponse<T>
            {
                StatusCode = code.ToString(),
                Message = message,
            };
        }
    }
}
