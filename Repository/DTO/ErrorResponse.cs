namespace Repository.DTO
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }

    public static class ErrorHelper
    {
        public static ErrorResponse Create(string errorCode, string message)
        {
            return new ErrorResponse
            {
                ErrorCode = errorCode,
                Message = message
            };
        }
    }


}   
