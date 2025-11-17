namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Configurations
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, _logger);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
        {
            context.Response.ContentType = "application/json";

            if (ex is ApiException apiEx)
            {
                context.Response.StatusCode = apiEx.StatusCode;
                await context.Response.WriteAsJsonAsync(new
                {
                    errorCode = apiEx.ErrorCode,
                    message = ex.Message
                });
            }
            else
            {
                logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new
                {
                    errorCode = "500",
                    message = "Internal server error"
                });
            }
        }
    }

}
