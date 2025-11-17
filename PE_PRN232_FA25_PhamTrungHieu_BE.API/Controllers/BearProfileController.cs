using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_PhamTrungHieu_BE.BAL.Services;
using PE_PRN232_FA25_PhamTrungHieu_BE.DAL.ModelExtensions;

namespace PE_PRN232_FA25_PhamTrungHieu_BE.API.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfileController : ControllerBase
    {
        private readonly IBearProfileService _service;

        public BearProfileController(IBearProfileService bearProfileService)
        {
            _service = bearProfileService;
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _service.GetByIdAsync(id);

            if (response == null)
                return NotFound(ErrorResponse.NotFound());

            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Create([FromBody] CreateBearProfileRequest request)
        {
            var errMsg = await _service.ValidateModelAsync(bearName: request.BearName, bearWeight: request.BearWeight);
            if (!string.IsNullOrEmpty(errMsg))
                return BadRequest(ErrorResponse.Invalid(errMsg));

            var response = await _service.CreateAsync(request);

            return Ok($"Created successfully with id {response}");
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBearProfileRequest request)
        {
            var errMsg = await _service.ValidateModelAsync(bearName: request.BearName, bearWeight: request.BearWeight);
            if (!string.IsNullOrEmpty(errMsg))
                return BadRequest(ErrorResponse.Invalid(errMsg));

            var response = await _service.UpdateAsync(id, request);
            if (!string.IsNullOrEmpty(response))
                return NotFound(ErrorResponse.NotFound());

            return Ok($"Updated successfully id {id}");
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await _service.DeleteAsync(id);
            if (!string.IsNullOrEmpty(response))
                return NotFound(ErrorResponse.NotFound());

            return Ok($"Deleted successfully id {id}");
        }
    }
}
