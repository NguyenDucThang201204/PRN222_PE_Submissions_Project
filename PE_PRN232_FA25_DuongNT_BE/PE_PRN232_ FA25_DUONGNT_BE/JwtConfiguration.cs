using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PE_PRN232__FA25_DUONGNT_BE
{
    public class JwtConfiguration
    {
        public static void ConfigureJwtEvents(JwtBearerOptions options)
        {
            options.Events = new JwtBearerEvents
            {
                OnChallenge = context =>
                {
                    context.HandleResponse();
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    var error = new
                    {
                        ErrorCode = "HB40101",
                        Status = 401,
                        Message = "Token missing/invalid"
                    };

                    return context.Response.WriteAsJsonAsync(error);
                },
                OnForbidden = context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";

                    var error = new
                    {
                        ErrorCode = "HB40301",
                        Status = 403,
                        Message = "Permission denied"
                    };

                    return context.Response.WriteAsJsonAsync(error);
                }
            };
        }
    }
}
