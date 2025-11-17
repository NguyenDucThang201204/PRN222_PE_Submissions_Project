using Microsoft.AspNetCore.Mvc;
using Business.Interfaces;
using Data.Entities;
using Application.EndPoints;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    [ApiController]
    public class BearProfileController : ControllerBase
    {
        private readonly IBearProfileService _service;

        public BearProfileController(IBearProfileService service)
        {
            _service = service;
        }

        [HttpGet(EndPoints.EndPoints.BearProfile.GetAll)]
        [Authorize] 
        public IActionResult GetAll()
        {
            var bears = _service.GetAll();
            return Ok(bears);
        }

        [HttpGet(EndPoints.EndPoints.BearProfile.GetById)]
        [Authorize]
        public IActionResult GetById(int id)
        {
            var bear = _service.GetById(id);
            if (bear == null)
                return NotFound();

            return Ok(bear);
        }

        [HttpPost(EndPoints.EndPoints.BearProfile.Create)]
        [Authorize]
        public IActionResult Add([FromBody] BearProfile bear)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Add(bear);
            return Created(EndPoints.EndPoints.BearProfile.GetById.Replace("{id}", bear.BearProfileId.ToString()), bear);
        }

        [HttpPut(EndPoints.EndPoints.BearProfile.Update)]
        [Authorize]
        public IActionResult Update(int id, [FromBody] BearProfile bear)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Update(id, bear);
            return Ok("1");
        }

        [HttpDelete(EndPoints.EndPoints.BearProfile.Delete)]
        [Authorize]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return Ok("1");
        }
    }
}
