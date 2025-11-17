using System.Net;
using System.Text.Json;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;

namespace PRN232_SU25_SE182634.api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await WriteErrorAsync(context, ex);
            }
        }

        private static async Task WriteErrorAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            var (httpStatus, apiCode, message) = MapException(ex);
            context.Response.StatusCode = (int)httpStatus;

            var payload = ApiResponse<string>.Error(apiCode, message);
            var json = JsonSerializer.Serialize(payload);
            await context.Response.WriteAsync(json);
        }

        private static (HttpStatusCode, ApiStatusCode, string) MapException(Exception ex)
        {
            return ex switch
            {
                ArgumentNullException ane => (HttpStatusCode.BadRequest, ApiStatusCode.HB40001, ane.Message),
                ArgumentException ae      => (HttpStatusCode.BadRequest, ApiStatusCode.HB40001, ae.Message),
                KeyNotFoundException kne  => (HttpStatusCode.NotFound, ApiStatusCode.HB40401, kne.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, ApiStatusCode.HB40101, "Unauthorized"),
                _ => (HttpStatusCode.InternalServerError, ApiStatusCode.HB50001, "Internal server error")
            };
        }
    }
}