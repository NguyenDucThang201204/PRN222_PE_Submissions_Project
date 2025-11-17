using Microsoft.AspNetCore.Mvc;

namespace PE_PRN232_FA25_PhanXuanPhu_API.Models
{
    public class ErrorHelper
    {
        public static IActionResult BadRequest(string message = "Missing/invalid input")
            => new BadRequestObjectResult(new ErrorResponse("HB40001", message));

        public static IActionResult Unauthorized()
            => new UnauthorizedObjectResult(new ErrorResponse("HB40101", "Token missing/invalid"));

        public static IActionResult Forbidden()
            => new ObjectResult(new ErrorResponse("HB40301", "Permission denied")) { StatusCode = 403 };

        public static IActionResult NotFound()
            => new NotFoundObjectResult(new ErrorResponse("HB40401", "Resource not found"));

        public static IActionResult InternalServer(string message = "Internal server error")
            => new ObjectResult(new ErrorResponse("HB50001", message)) { StatusCode = 500 };
    }
}
