namespace PE_PRN232_FA25_PhamThiThanhNgan.API.Commons
{
    public class ErrorResponse
    {
        public string ErrorCode { get; set; } = null!;
        public string Message { get; set; } = null!;

        private ErrorResponse(string errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public static ErrorResponse InvalidInput(string message)
            => new ErrorResponse("HB40001", message);
        public static ErrorResponse TokenInvalid(string message)
            => new ErrorResponse("HB40101", message);
        public static ErrorResponse PermissionDenied(string message)
            => new ErrorResponse("HB40301", message);
        public static ErrorResponse ResourceNotFound(string message)
            => new ErrorResponse("HB40401", message);
        public static ErrorResponse InternalServerError(string message)
            => new ErrorResponse("HB50001", message);
    }
}
