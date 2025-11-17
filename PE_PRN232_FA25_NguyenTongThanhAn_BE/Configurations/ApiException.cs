namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Configurations
{
    public class ApiException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public ApiException(string errorCode, string message, int statusCode = 500)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }

    //400
    public class BadRequestException : ApiException
    {
        public BadRequestException(string message = "Missing/invalid input")
            : base("400", message, StatusCodes.Status400BadRequest) { }
    }


    //401
    public class UnauthorizedException : ApiException
    {
        public UnauthorizedException(string message = "Token missing/invalid")
            : base("401", message, StatusCodes.Status401Unauthorized) { }
    }


    //403
    public class ForbiddenException : ApiException
    {
        public ForbiddenException(string message = "Permission denied")
            : base("403", message, StatusCodes.Status403Forbidden) { }
    }
    //404
    public class NotFoundException : ApiException
    {
        public NotFoundException(string message = "Resource not found")
            : base("404", message, StatusCodes.Status404NotFound) { }
    }
    //500
    public class InternalServerException : ApiException
    {
        public InternalServerException(string message = "Internal server error")
            : base("500", message, StatusCodes.Status500InternalServerError) { }
    }

}
