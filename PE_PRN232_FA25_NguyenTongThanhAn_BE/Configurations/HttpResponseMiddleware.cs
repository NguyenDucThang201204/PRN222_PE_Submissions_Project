using System.Text.Json;
using System.Text.Json.Nodes;

namespace PE_PRN232_FA25_NguyenTongThanhAn_BE.Configurations
{
    public class HttpResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpResponseMiddleware> _logger;

        public HttpResponseMiddleware(RequestDelegate next, ILogger<HttpResponseMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Seek(0, SeekOrigin.Begin);

            if (context.Response.StatusCode >= 400)
            {
                _logger.LogWarning("Intercepted {StatusCode} response", context.Response.StatusCode);

                string? existingBody = await new StreamReader(memStream).ReadToEndAsync();
                memStream.SetLength(0); // reset

                string? customMessage = null;

                if (!string.IsNullOrWhiteSpace(existingBody) && existingBody.TrimStart().StartsWith("{"))
                {
                    try
                    {
                        var json = JsonNode.Parse(existingBody);
                        customMessage = json?["message"]?.ToString();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning(ex, "Failed to parse existing JSON body");
                    }
                }
                else
                {
                    _logger.LogWarning("Response body is not JSON, skipping parse: {Body}", existingBody);
                }

                var (code, defaultMessage) = ErrorCodeMapper.Get(context.Response.StatusCode);

                var finalResponse = new
                {
                    errorCode = code,
                    message = !string.IsNullOrWhiteSpace(customMessage) ? customMessage : defaultMessage
                };

                context.Response.ContentType = "application/json";
                await JsonSerializer.SerializeAsync(memStream, finalResponse);
            }

            memStream.Seek(0, SeekOrigin.Begin);
            await memStream.CopyToAsync(originalBody);
            context.Response.Body = originalBody;
        }
    }

}
