using BOs.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Services;

namespace PE_PRN232_FA25_NguyenThiMaiTrinh_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ODataController
    {
        private readonly IProfileService _profileService;
        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [EnableQuery]
        [Authorize(Roles = "2")]
        [HttpGet("/api/BearProfiles")]
        public async Task<ActionResult<IEnumerable<BearProfile>>> GetProfiles()
        {
            try
            {
                var profiles = await _profileService.GetProfiles();
                return Ok(profiles);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [Authorize(Roles = "2")]
        [HttpGet("/api/BearTypes")]
        public async Task<ActionResult<List<BearType>>> GetType()
        {
            try
            {
                var types = await _profileService.GetTypes();
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }
        [HttpGet("/api/BearProfiles/{id}")]
        [Authorize(Roles = "2")]

        public async Task<ActionResult<BearProfile>> GetProfile(int id)
        {
            try
            {
                var profile = await _profileService.GetProfile(id);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpPost("/api/BearProfiles")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<BearProfile>> AddProfile([FromBody] BearProfile profile)
        {
            try
            {
                var newProfile = await _profileService.AddProfile(profile);
                return Ok(newProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [HttpPut("/api/BearProfiles/{id}")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<BearProfile>> UpdateProfile(int id, [FromBody] BearProfile profile)
        {
            try
            {
                var updatedProfile = await _profileService.UpdateProfile(profile);
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }



        [HttpDelete("/api/BearProfiles/{id}")]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<BearProfile>> DeleteProfile(int id)
        {
            try
            {
                var deletedProfile = await _profileService.DeleteProfile(id);
                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"{ex.Message}");
            }
        }

        [Authorize(Roles = "2,3,4")]
        [HttpGet("search")]
        public async Task<IActionResult> SearchProfiles([FromQuery] string keyword)
        {
            var result = await _profileService.SearchProfile(keyword);

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
