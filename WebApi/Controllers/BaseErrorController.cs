using Microsoft.AspNetCore.Mvc;
using Repository.DTO;

namespace WebApi.api.Controllers
{
    [ApiController]
    public class BaseErrorController : Controller
    {
        protected IActionResult Error400(string? message = null)
                => BadRequest(ErrorHelper.Create("HB40001", message ?? "Missing/invalid input"));

        protected IActionResult Error401(string? message = null)
            => Unauthorized(ErrorHelper.Create("HB40101", message ?? "Token missing/invalid"));

        protected IActionResult Error403(string? message = null)
            => StatusCode(403, ErrorHelper.Create("HB40301", message ?? "Permission denied"));

        protected IActionResult Error404(string? message = null)
            => NotFound(ErrorHelper.Create("HB40401", message ?? "Resource not found"));

        protected IActionResult Error500(string? message = null)
            => StatusCode(500, ErrorHelper.Create("HB50001", message ?? "Internal server error"));
    }
}
