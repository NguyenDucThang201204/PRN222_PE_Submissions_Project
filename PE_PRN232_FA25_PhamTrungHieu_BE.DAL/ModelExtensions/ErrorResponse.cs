namespace PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; }
        public string Message { get; set; }

        public static ErrorResponse Invalid(string? newMessage = "")
        {
            return new ErrorResponse
            {
                ErrorCode = "HB40001",
                Message = !string.IsNullOrEmpty(newMessage) ? newMessage : "Missing/invalid input"
            };
        }
        public static ErrorResponse Unauthor(string? newMessage = "")
        {
            return new ErrorResponse
            {
                ErrorCode = "HB40101",
                Message = !string.IsNullOrEmpty(newMessage) ? newMessage : "Token missing/invalid"
            };
        }
        public static ErrorResponse Deny(string? newMessage = "")
        {
            return new ErrorResponse
            {
                ErrorCode = "HB40301",
                Message = !string.IsNullOrEmpty(newMessage) ? newMessage : "Permission denied"
            };
        }
        public static ErrorResponse NotFound(string? newMessage = "")
        {
            return new ErrorResponse
            {
                ErrorCode = "HB40401",
                Message = !string.IsNullOrEmpty(newMessage) ? newMessage : "Resource not found"
            };
        }
        public static ErrorResponse InternalException(string? newMessage = "")
        {
            return new ErrorResponse
            {
                ErrorCode = "HB50001",
                Message = !string.IsNullOrEmpty(newMessage) ? newMessage : "Internal server error"
            };
        }
    }

}
