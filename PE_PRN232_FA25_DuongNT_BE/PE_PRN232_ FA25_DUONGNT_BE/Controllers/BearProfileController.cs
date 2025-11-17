using BAL.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232__FA25_DUONGNT_BE.RequestModels;

namespace PE_PRN232__FA25_DUONGNT_BE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BearProfileController : Controller
    {
        private readonly BearProfileService _service;

        public BearProfileController(BearProfileService service)
        {
            _service = service;
        }

        /// <summary>
        /// GET /api/handbags - List all handbags with brand info
        /// Roles: administrator, moderator, developer, member
        /// Status: 200, 401, 403
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAllHandbags()
        {
            try
            {
                var handbags = await _service.GetAllAsync();
                return Ok(handbags);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiErrorResponse
                {
                    ErrorCode = "HB50001",
                    Status = 500,
                    Message = "Internal server error"
                });
            }
        }

        /// <summary>
        /// GET /api/handbags/{id} - Get handbag by ID
        /// Roles: administrator, moderator, developer, member
        /// Status: 200, 404, 401, 403
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetHandbagById(int id)
        {
            try
            {
                // Validation: ID must be positive
                if (id <= 0)
                {
                    return BadRequest(new ApiErrorResponse
                    {
                        ErrorCode = "HB40001",
                        Status = 400,
                        Message = "Missing/invalid input"
                    });
                }

                var handbag = await _service.GetByIdAsync(id);

                if (handbag == null)
                {
                    return NotFound(new ApiErrorResponse
                    {
                        ErrorCode = "HB40401",
                        Status = 404,
                        Message = "Resource not found"
                    });
                }

                return Ok(handbag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiErrorResponse
                {
                    ErrorCode = "HB50001",
                    Status = 500,
                    Message = "Internal server error"
                });
            }
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] CreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                {
                    errorCode = "HB40001",
                    message = "Invalid input"
                });

            var result = await _service.CreateAsync(req);

            return StatusCode(result.statusCode, new
            {
                errorCode = result.errorCode,
                message = result.message,
                data = result.data
            });
        }
        // ✅ PUT: /api/handbags/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateRequest req)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errorCode = "HB40001", message = "Invalid input" });

            var result = await _service.UpdateAsync(id, req);
            return StatusCode(result.statusCode, new
            {
                errorCode = result.errorCode,
                message = result.message,
                data = result.data
            });
        }
        // ✅ DELETE: /api/handbags/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            return StatusCode(result.statusCode, new
            {
                errorCode = result.errorCode,
                message = result.message
            });
        }
        [HttpGet("search")]
        [Authorize] // tất cả role cần token
        public async Task<IActionResult> Search([FromQuery] string? leopardName, [FromQuery] double? weight)
        {
            try
            {
                // 401 - token invalid/missing
                if (!User.Identity?.IsAuthenticated ?? false)
                    return Unauthorized(new { code = "HB40101", message = "Token missing/invalid" });

                // 400 - missing input
                if (string.IsNullOrWhiteSpace(leopardName) && !weight.HasValue)
                    return BadRequest(new { code = "HB40001", message = "Missing/invalid input" });

                // gọi service
                var result = await _service.SearchAsync(leopardName, weight); // IEnumerable<object>

                // 404 - not found
                if (result == null || !result.Any())
                    return NotFound(new { code = "HB40401", message = "Resource not found" });

                // 200 - success
                return Ok(new { status = "200", message = "Search successful", data = result });
            }
            catch (UnauthorizedAccessException)
            {
                // 403 - permission denied
                return StatusCode(403, new { code = "HB40301", message = "Permission denied" });
            }
            catch (Exception ex)
            {
                // 500 - internal server error
                // Log ex.Message, ex.StackTrace nếu cần
                return StatusCode(500, new { code = "HB50001", message = "Internal server error" });
            }
        }
    }
}
