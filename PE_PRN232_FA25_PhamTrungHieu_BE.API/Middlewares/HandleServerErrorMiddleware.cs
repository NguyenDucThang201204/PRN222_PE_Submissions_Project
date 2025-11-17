using System.Net;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API.Extensions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HandleServerErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleServerErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(ErrorResponse.InternalException());
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HandleServerErrorMiddlewareExtensions
    {
        public static IApplicationBuilder UseHandleServerErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HandleServerErrorMiddleware>();
        }
    }
}
