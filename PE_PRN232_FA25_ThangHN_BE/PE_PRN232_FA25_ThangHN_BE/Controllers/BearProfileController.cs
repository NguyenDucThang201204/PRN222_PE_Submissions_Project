using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Query.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Repository.Models;
using Service;

namespace PE_PRN232_FA25_ThangHN_BE.Controllers
{
    [ApiController]
    [Route("BearProfiles")]
    [Authorize]
    public class BearProfileController : ControllerBase
    {
        private readonly BearProfileService _service;
        private readonly BearTypeService _typeService;
        public BearProfileController(BearProfileService service, BearTypeService typeService)
        {
            _service = service;
            _typeService = typeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BearProfile>>> GetAll()
        {
            return await _service.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BearProfile>> GetById(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
                throw new KeyNotFoundException($"ID {id} not found");

            return entity;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteAsync(id);

            if (!success)
                throw new KeyNotFoundException($"ID {id} not found");

            return Ok(new { message = "Deleted successfully" });
        }


        [HttpGet("search-group-by")]
        public async Task<IActionResult> Search([FromQuery] string? bearName, [FromQuery] double? bearWeight, [FromQuery] string? bearTypeName)
        {
            var handbags = await _service.SearchAsync(bearName, bearWeight, bearTypeName);

            var groupedHandbags = handbags
                .GroupBy(h => h.BearType?.BearTypeName ?? "Unknown")
                .ToList();

            return Ok(groupedHandbags);
        }


        [HttpGet("search-no-paging")]
        public async Task<ActionResult<IEnumerable<BearProfile>>> SearchNoPaging(string? bearName, double? bearWeight, string? bearTypeName)
        {
            try
            {
                return await _service.SearchAsync(bearName, bearWeight, bearTypeName);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
