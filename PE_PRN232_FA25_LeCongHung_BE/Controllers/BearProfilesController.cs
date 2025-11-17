using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeCongHung_BLL.DTOs;
using PE_PRN232_FA25_LeCongHung_BLL.Services;
using System.Security.Claims;

namespace PE_PRN232_FA25_LeCongHung_BE.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BearProfilesController : ControllerBase
    {
        private readonly IBearProfileService _bearProfileService;
        private readonly ILogger<BearProfilesController> _logger;

        public BearProfilesController(IBearProfileService bearProfileService, ILogger<BearProfilesController> logger)
        {
            _bearProfileService = bearProfileService;
            _logger = logger;
        }

        // POST /BearProfiles
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Create([FromBody] BearProfileCreateDTO dto)
        {
            try
            {
                var result = await _bearProfileService.CreateAsync(dto);
                if (result == null)
                {
                    return BadRequest("Failed to create bear profile.");
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating bear profile");
                return StatusCode(500, "Internal server error");
            }
        }

        // PUT /BearProfiles
        [HttpPut]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Update([FromBody] BearProfileUpdateDTO dto)
        {
            try
            {
                var result = await _bearProfileService.UpdateAsync(dto);
                if (result == null)
                {
                    return NotFound("Bear profile not found.");
                }
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating bear profile");
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE /BearProfiles/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _bearProfileService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Bear profile not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting bear profile");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET /BearProfiles
        [HttpGet]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _bearProfileService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all bear profiles");
                return StatusCode(500, "Internal server error");
            }
        }

        // GET /BearProfiles/{id}
        [HttpGet("{id}")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _bearProfileService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound("Bear profile not found.");
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting bear profile by id");
                return StatusCode(500, "Internal server error");
            }
        }

        // POST /BearProfiles/Search
        [HttpPost("Search")]
        [Authorize]
        public async Task<IActionResult> Search([FromBody] SearchRequestDTO request)
        {
            try
            {
                var roleId = GetUserRoleId();
                // Only Manager (1), Staff (2), or Member (3) can search
                if (roleId != 1 && roleId != 2 && roleId != 3)
                {
                    return Forbid("You do not have permission to access this resource.");
                }

                var result = await _bearProfileService.SearchAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching bear profiles");
                return StatusCode(500, "Internal server error");
            }
        }

        private int GetUserRoleId()
        {
            var roleIdClaim = User.FindFirst("RoleId")?.Value;
            if (int.TryParse(roleIdClaim, out int roleId))
            {
                return roleId;
            }
            return 0;
        }
    }
}

