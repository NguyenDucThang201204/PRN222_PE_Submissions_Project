using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_DinhLeThinh_API.Middleware;
using Service;

namespace PE_PRN232_FA25_DinhLeThinh_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly IBearService _bear;

        public BearProfilesController(IBearService bear)
        {
            _bear = bear;
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetAll()
        {
            var bears = await _bear.GetAllBear();
            return Ok(bears);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetById(int id)
        {
            var bear = await _bear.GetByIdAsync(id);
            if (bear == null)
            {
                return NotFound(ErrorResponse.FromErrorCode(ErrorCode.HB40401, "Bear not found"));
            }
            return Ok(bear);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> DeleteById(int id)
        {
            var bear = await _bear.GetByIdAsync(id);
            if (bear == null)
            {
                return NotFound(ErrorResponse.FromErrorCode(ErrorCode.HB40401, "Bear not found"));
            }
            var res = await _bear.DeleteByIdAsync(id);
            if (!res) return BadRequest(ErrorResponse.FromErrorCode(ErrorCode.HB40001, "Deletion failed"));
            return Ok();
        }
    }
}
