using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE__PRN232_FA25_NamPK_BLL;
using PE__PRN232_FA25_NamPK_BLL.DTO;
using System.Text.RegularExpressions;

namespace PE__PRN232_FA25_NamPK_BE.Controllers
{
    [Authorize]
    [ApiController]
    [Route("BearProfiles")]
    public class BearProfileController : Controller
    {
        private readonly BearProfileService _bearProfileService;

        public BearProfileController(BearProfileService bearProfileService)
        {
            _bearProfileService = bearProfileService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _bearProfileService.GetAllAsync();

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var response = await _bearProfileService.GetByIdAsync(id);

            if (response == null)
            {
                return NotFound("Bear profile not found");
            }

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> CreateHandBagAsync([FromBody] BearProfileRequest bearProfile)
        {
            if (!Regex.IsMatch(bearProfile.BearName, @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$"))
            {
                return BadRequest("Bear name must start with a capital letter or number, and can contain letters, numbers, or #.");
            }
            else if (bearProfile.BearWeight == null || bearProfile.BearWeight <= 0)
            {
                return BadRequest("weight need to biggest than 200");
            }
            await _bearProfileService.CreateHandBagAsync(bearProfile);
            return Ok("Create Sucessfully");
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] BearProfileRequest bearProfile)
        {
            if (!Regex.IsMatch(bearProfile.BearName, @"^([A-Z0-9][a-zA-Z0-9#]*\s)*([A-Z0-9][a-zA-Z0-9#]*)$"))
            {
                return BadRequest("Bear name must start with a capital letter or number, and can contain letters, numbers, or #.");
            }
            else if (bearProfile.BearWeight == null || bearProfile.BearWeight <= 0)
            {
                return BadRequest("weight need to biggest than 200");
            }
            var existing = await _bearProfileService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("bearProfile not found.");

            await _bearProfileService.UpdateHandBagAsync(id, bearProfile);
            return Ok(new { message = " updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1,2")]
        public async Task<IActionResult> DeleteHandBagAsync(int id)
        {
            var existing = await _bearProfileService.GetByIdAsync(id);
            if (existing == null)
                return NotFound("bearProfile not found.");

            await _bearProfileService.DeleteHandBagAsync(existing);
            return Ok(new { message = "bearProfile deleted successfully." });
        }

    }
}
