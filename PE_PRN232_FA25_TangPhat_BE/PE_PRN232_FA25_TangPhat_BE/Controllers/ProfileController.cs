using BusinessObjects.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Service;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace PRN_SU25_SE181923.api.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profile;
        public ProfileController(IProfileService profile)
        {
            _profile = profile;
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetAll()
        {
            var profiles = await _profile.GetAllAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetById(int id)
        {
            var profile = await _profile.GetByIdAsync(id);
            if (profile == null)
            {
                return NotFound(ErrorResponse.Fail(ErrorCode.HB40401, "Profile not found"));
            }
            return Ok(profile);
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Create([FromBody] CreateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Missing/invalid input"));
            }

            var namePattern = @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$";
            if (!Regex.IsMatch(request.BearName, namePattern))
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Invalid name"));
            }

            if (request.Weight <= 15)
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Weight must be greater than 200"));
            }

            var created = await _profile.CreateAsync(request);
            if (created == null) return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Creation failed"));
            return Ok(created);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Missing/invalid input"));
            }

            var handbag = await _profile.GetByIdAsync(id);
            if (handbag == null)
            {
                return NotFound(ErrorResponse.Fail(ErrorCode.HB40401, "Profile not found"));
            }

            var namePattern = @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$";
            if (!Regex.IsMatch(request.BearName, namePattern))
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Invalid name"));
            }

            if (request.Weight <= 200 )
            {
                return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Weight must be greater than 200"));
            }

            var updatedHandbag = await _profile.UpdateAsync(id, request);
            if (updatedHandbag == null) return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Update failed"));
            return Ok(updatedHandbag);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteHandbag(int id)
        {
            var handbag = await _profile.GetByIdAsync(id);
            if (handbag == null)
            {
                return NotFound(ErrorResponse.Fail(ErrorCode.HB40401, "Profile not found"));
            }
            var result = await _profile.DeleteAsync(id);
            if (!result) return BadRequest(ErrorResponse.Fail(ErrorCode.HB40001, "Deletion failed"));
            return Ok();
        }

    }
}
