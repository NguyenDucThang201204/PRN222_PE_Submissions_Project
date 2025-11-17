using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Request;
using PE_PRN232_FA25_AnhTHQ_BLL.DTOs.Response;
using PE_PRN232_FA25_AnhTHQ_BLL.Services;

namespace PE_PRN232_FA25_AnhTHQ_BE.Controllers
{
    [ApiController]
    [Route("/BearProfiles")]
    [Authorize] // all endpoints require auth
    public class BearProfileController : ControllerBase
    {
        private readonly BearProfileService _service;

        public BearProfileController(BearProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _service.GetAllsAsync();
                if (items == null || items.Count == 0)
                    return NotFound(ApiResponse<List<BearDto>>.Error(ApiStatusCode.HB40401, "No items found."));

                return Ok(ApiResponse<List<BearDto>>.Success(items));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
                return BadRequest(ApiResponse<string>.Error(ApiStatusCode.HB40001, "Invalid Profile ID."));
            try
            {
                var items = await _service.GetByIdAsync(id);
                if (items != null)
                    return Ok(ApiResponse<BearDto>.Success(items));

                return NotFound(ApiResponse<string>.Error(ApiStatusCode.HB40401, $"Profile with ID {id} not found."));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.Error(ApiStatusCode.HB40401, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }

        [HttpPost("/Search")]
        // using relative search
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> SearchAsync([FromBody] SearchRequest request)
        {
            if (request == null || (string.IsNullOrWhiteSpace(request.bearName) && Double.IsPositive(request.bearWeight) && string.IsNullOrWhiteSpace(request.bearTypeName)))
                return BadRequest(ApiResponse<string>.Error(ApiStatusCode.HB40001, "At least one search parameter must be provided."));
            try
            {
                var results = await _service.SearchAsync(request.bearName, request.bearWeight, request.bearTypeName);
                if (results == null)
                    return NotFound(ApiResponse<string>.Error(ApiStatusCode.HB40401, "No profiles match criteria."));
                return Ok(ApiResponse<PagedResult<BearDto>>.Success(results, "Search success."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Create([FromBody] CreateProfileRequest dto)
        {
            try
            {
                var created = await _service.CreateAsync(dto);
                return Ok(ApiResponse<BearDto>.Success(created, "Create success."));
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ApiResponse<string>.Error(ApiStatusCode.HB40001, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Update(int id, [FromBody] BearDto dto)
        {
            try
            {
                var updated = await _service.UpdateAsync(id, dto);
                return Ok(ApiResponse<BearDto>.Success(updated, "Update success."));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.Error(ApiStatusCode.HB40401, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _service.DeleteAsync(id);
                return Ok(ApiResponse<string>.Success(null, "Delete success."));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponse<string>.Error(ApiStatusCode.HB40401, ex.Message));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error(ApiStatusCode.HB50001, ex.Message));
            }
        }
    }
}
