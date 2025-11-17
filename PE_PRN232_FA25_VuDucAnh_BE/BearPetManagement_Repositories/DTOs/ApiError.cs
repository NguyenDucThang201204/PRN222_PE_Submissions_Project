using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearPetManagement_Repositories.DTOs
{
    public class ApiError
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public ApiError(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public static readonly Dictionary<int, ApiError> _errors = new()
        {
            {400, new ApiError("400", "Missing/invalid input")},
            {401, new ApiError("401", "Token missing/invalid")},
            {403, new ApiError("403", "Permission denied")},
            {404, new ApiError("404", "Resource not found")},
            {500, new ApiError("500", "Internal server error")}


        };
    }
}
