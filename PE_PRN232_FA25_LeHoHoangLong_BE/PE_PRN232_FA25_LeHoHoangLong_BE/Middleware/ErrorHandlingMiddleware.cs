using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PE_PRN232_FA25_LeHoHoangLong_BE.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (!context.Response.HasStarted && context.Response.ContentLength == null)
                {
                    if (context.Response.StatusCode == 401)
                    {
                        await HandleUnauthorizedAsync(context);
                    }
                    else if (context.Response.StatusCode == 403)
                    {
                        await HandleForbiddenAsync(context);
                    }
                    else if (context.Response.StatusCode == 404)
                    {
                        await HandleNotFoundAsync(context);
                    }
                }
            }
            catch (Exception ex)
            {
                if (!context.Response.HasStarted)
                {
                    await HandleExceptionAsync(context);
                }
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = ErrorResponse.FromErrorCode(
                ErrorCode.HB50001,
                "Internal server error");

            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private static async Task HandleUnauthorizedAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = ErrorResponse.FromErrorCode(
                ErrorCode.HB40101,
                "Missing/invalid input");

            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private static async Task HandleForbiddenAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = ErrorResponse.FromErrorCode(
                ErrorCode.HB40301,
                "Permission denied");

            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }

        private static async Task HandleNotFoundAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = ErrorResponse.FromErrorCode(
                ErrorCode.HB40401,
                "Resource not found");

            context.Response.StatusCode = response.StatusCode;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
        }
    }

    public enum ErrorCode
    {
        HB40001,
        HB40101,
        HB40301,
        HB40401,
        HB50001,
    }

    public class ErrorResponse
    {
        [JsonPropertyName("errorCode")]
        public string ErrorCodeString { get; set; } = string.Empty;

        [JsonIgnore]
        public int StatusCode { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        public static ErrorResponse FromErrorCode(ErrorCode errorCode, string message)
        {
            var statusCode = GetHttpStatusCode(errorCode);

            return new ErrorResponse
            {
                ErrorCodeString = errorCode.ToString(),
                StatusCode = (int)statusCode,
                Message = message
            };
        }

        private static HttpStatusCode GetHttpStatusCode(ErrorCode errorCode)
        {
            return errorCode switch
            {
                ErrorCode.HB40001 => HttpStatusCode.BadRequest,
                ErrorCode.HB40101 => HttpStatusCode.Unauthorized,
                ErrorCode.HB40301 => HttpStatusCode.Forbidden,
                ErrorCode.HB40401 => HttpStatusCode.NotFound,
                ErrorCode.HB50001 => HttpStatusCode.InternalServerError,
                _ => HttpStatusCode.InternalServerError
            };
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
