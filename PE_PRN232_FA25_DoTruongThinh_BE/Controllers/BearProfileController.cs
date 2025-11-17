using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repo.DTO;
using Service;

namespace PE_PRN232_FA25_DoTruongThinh_BE.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfileController : ControllerBase
    {
        private readonly ProfileService _service;
        public BearProfileController(ProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Manager, Staff, Member")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var all = await _service.GetAll();
                return Ok(all);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("500", "Internal server error"));
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Manager, Staff, Member")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var dto = await _service.GetById(id);
                if (dto == null)
                    return NotFound(new ErrorResponse("404", "Resource not found"));
                return Ok(dto);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("500", "Internal server error"));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .FirstOrDefault() ?? "Invalid input";
                return BadRequest(new ErrorResponse("400", error));
            }

            try
            {
                int result = await _service.Create(request);
                return CreatedAtAction(nameof(Get), new { id = result }, new { message = "Created successfully." });
                
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("500", "Internal server error"));
            }
        }

       

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var success = await _service.Delete(id);
                if (!success)
                    return NotFound(new ErrorResponse("404", "Resource not found"));
                return Ok(new { message = "Deleted successfully." });
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse("500", "Internal server error"));
            }
        }

        
    }
}
