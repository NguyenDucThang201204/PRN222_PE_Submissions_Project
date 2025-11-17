using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using PE_PRN232_FA25_NguyenThaiDuy_BE.Repositories;
using PE_PRN232_FA25_NguyenThaiDuy_BE.services;
using System.Text.RegularExpressions;

namespace PE_PRN232_FA25_NguyenThaiDuy_BE.api.Controllers
{
    [Route("api/BearProfile")]
    [ApiController]
    public class BearsController : ControllerBase
    {
        private readonly IBearProfileService _bearProfileService;

        public BearsController(IBearProfileService bearProfileService)
        {
            _bearProfileService = bearProfileService;
        }

        [HttpGet]
        [Authorize(Policy = "ReadOnly")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var profiles = await _bearProfileService.GetAllAsync();
                return Ok(profiles);
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "ReadOnly")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var profile = await _bearProfileService.GetByIdAsync(id);
                if (profile == null)
                    return NotFound(new { errorCode = "HB40401", message = "BearProfile not found" });

                return Ok(profile);
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpPost]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Create([FromBody] BearProfileDto dto)
        {
            if (dto == null)
                return BadRequest(new { errorCode = "HB40001", message = "Request body is required" });

            var regex = new Regex(@"^([A-Z0-9][a-zA-Z0-9]*\s)*([A-Z0-9][a-zA-Z0-9]*)$");
            if (!regex.IsMatch(dto.BearName ?? ""))
                return BadRequest(new { errorCode = "HB40001", message = "Invalid BearName format" });

            if (dto.BearWeight <= 200)
                return BadRequest(new { errorCode = "HB40001", message = "Weight must be greater than 200" });

            try
            {
                var newProfile = await _bearProfileService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = newProfile.BearProfileId }, newProfile);
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Update(int id, [FromBody] BearProfileDto dto)
        {
            if (dto == null)
                return BadRequest(new { errorCode = "HB40001", message = "Request body is required" });

            try
            {
                var updated = await _bearProfileService.UpdateAsync(id, dto);
                if (!updated)
                    return NotFound(new { errorCode = "HB40401", message = "BearProfile not found" });

                return Ok(new { message = "BearProfile updated successfully" });
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "FullAccess")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _bearProfileService.DeleteAsync(id);
                if (!deleted)
                    return NotFound(new { errorCode = "HB40401", message = "BearProfile not found" });

                return Ok(new { message = "BearProfile deleted successfully" });
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpGet("search")]
        [Authorize(Policy = "ReadOnly")]
        [EnableQuery]
        public async Task<IActionResult> Search([FromQuery] string? BearName, [FromQuery] double? BearWeight)
        {
            try
            {
                var profiles = await _bearProfileService.GetAllAsync();
                var query = profiles.AsQueryable();

                if (!string.IsNullOrEmpty(BearName))
                    query = query.Where(l => l.BearName.Contains(BearName));

                if (BearWeight.HasValue)
                    query = query.Where(l => l.BearWeight == BearWeight.Value);

                return Ok(query);
            }
            catch
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }


    }
}

