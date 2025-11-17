using BO.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using System.Security.Claims;

namespace Bear.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly IBearProfileService _service;

        public BearProfilesController (IBearProfileService service)
        {
            _service = service;
        }
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
                throw new UnauthorizedAccessException("User ID not found or invalid");
            return userId;
        }
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public IActionResult CreateBear([FromBody] BearDto h)
        {
            try
            {
                var userId = GetCurrentUserId();

                if (h == null)
                    return BadRequest();

                var result = _service.CreateBear(h);
                return StatusCode(201, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (ArgumentException)
            {
                return BadRequest();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Manager, Staff")]

        public IActionResult GetBearById(int id)
        {
            try
            {
                var result = _service.GetBearProfileById(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Manager, Staff")]

        public IActionResult GetAllBear()
        {
            try
            {
                var result = _service.GetBearList();
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
