using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Dtos;
using Service.Security;

namespace PRN232_SU25_SE184826.api.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfileController(BearService _service) : ControllerBase
    {
        [HttpGet("BearProfiles")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAll();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetById(id);
            if (result == null)
                return NotFound(ErrorResponse.HB40401("Profile not found"));
            return Ok(result);
        }

        [HttpPost("BearProfiles")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Create([FromBody] CreateDto request)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Invalid input";
                return BadRequest(ErrorResponse.HB40001(error));
            }
            var result = await _service.Create(request);
            if (!result.Success)
            {
                if (result.ErrorCode == "HB40001")
                    return BadRequest(ErrorResponse.HB40001(result.ErrorMessage!));
                if (result.ErrorCode == "HB40301")
                    return StatusCode(403, ErrorResponse.HB40301(result.ErrorMessage!));
            }
            return CreatedAtAction(nameof(GetById), new { id = result.Data!.BearProfileId }, result.Data);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDto request)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Invalid input";
                return BadRequest(ErrorResponse.HB40001(error));
            }
            var result = await _service.Update(id, request);
            if (!result.Success)
            {
                if (result.ErrorCode == "HB40001")
                    return BadRequest(ErrorResponse.HB40001(result.ErrorMessage!));
                if (result.ErrorCode == "HB40401")
                    return NotFound(ErrorResponse.HB40401(result.ErrorMessage!));
                if (result.ErrorCode == "HB40301")
                    return StatusCode(403, ErrorResponse.HB40301(result.ErrorMessage!));
            }
            return Ok(result.Data);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "manager")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (!result.Success)
            {
                if (result.ErrorCode == "HB40401")
                    return NotFound(ErrorResponse.HB40401(result.ErrorMessage!));
                if (result.ErrorCode == "HB40301")
                    return StatusCode(403, ErrorResponse.HB40301(result.ErrorMessage!));
            }
            return Ok();
        }

        //[HttpGet("search")]
        //[Authorize]
        //public async Task<IActionResult> Search([FromQuery] string? bearName, [FromQuery] double? bearWeight, [FromQuery] string? bearTypeName)
        //{
        //    var result = await _service.Search(bearName, bearWeight, bearTypeName);
        //    return Ok(result.AsQueryable());
        //}

        //[HttpGet("paged")]
        //[Authorize(Roles = "manager")]
        //public async Task<IActionResult> GetPaged(
        //    [FromQuery] int page = 1, 
        //    [FromQuery] int pageSize = 3)
        //{
        //    if (page <= 0 || pageSize <= 0)
        //        return BadRequest(ErrorResponse.HB40001("Page and pageSize must be greater than 0."));

        //    var result = await _service.GetAllPaging(page, pageSize);
        //    return Ok(result);
        //}

        [HttpGet("Search")]
        [Authorize]
        public async Task<IActionResult> SearchWithPaging(
            [FromQuery] int page = 2, 
            [FromQuery] int pageSize = 3, 
            [FromQuery] string? bearName = null, 
            [FromQuery] double? weight = null,
            [FromQuery] string? bearTypeName = null)
        {
            var result = await _service.SearchWithPaging(page, pageSize, bearName, weight, bearTypeName);
            return Ok(result);
        }
    }
}
