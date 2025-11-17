using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositores.Models;
using Repositories.ModelExtensions;
using Services;
using Services.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfilesControlller : ControllerBase
    {
        private readonly BearProfileService _service;
        private readonly BearTypeService _serviceType;


        public BearProfilesControlller(BearProfileService service, BearTypeService serviceType)
        {
            _service = service;
            _serviceType = serviceType;
        }
        [Authorize(Roles = "2,3,4")]
        [HttpGet]
        public async Task<IEnumerable<BearProfile>> Get()
        {
            return await _service.GetAllAsync();
        }

        [Authorize(Roles = "2")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound(ErrorResponse.HB40401("Resource not found"));
            }
            return Ok(item);
        }

        [Authorize(Roles = "2")]
        [HttpPost]
        public async Task<IActionResult> Post(BearProfileRequest request)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Missing/Invalid input";
                return BadRequest(ErrorResponse.HB40001(error));
            }

            if (await _serviceType.GetByIdAsync(request.BearTypeId) == null)
            {
                return BadRequest(ErrorResponse.HB40001("Invalid input"));
            }

            var item = new BearProfile()
            {
                BearName = request.BearName,
                CareNeeds = request.CareNeeds,
                Characteristics = request.Characteristics,
                BearTypeId = request.BearTypeId,
                Weight = request.Weight,
                ModifiedDate = request.ModifiedDate
            };
            return Ok(await _service.CreateAsync(item));
        }

        [Authorize(Roles = "2")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(BearProfileUpdateRequest request, int id)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Missing/Invalid input";
                return BadRequest(ErrorResponse.HB40001(error));
            }

            var existing = await _service.GetByIdAsync(id);

            if (existing == null)
            {
                return NotFound(ErrorResponse.HB40401("Resource not found"));
            }

            if (await _serviceType.GetByIdAsync(request.BearTypeId) == null)
            {
                return BadRequest(ErrorResponse.HB40001("Invalid input"));
            }

            existing.BearName = request.BearName;
            existing.CareNeeds = request.CareNeeds;
            existing.Weight = request.Weight;
            existing.Characteristics = request.Characteristics;
            existing.BearTypeId = request.BearTypeId;
            existing.ModifiedDate = request.ModifiedDate;
            existing.BearType = null;
            return Ok(await _service.UpdateAsync(existing));
        }

        [Authorize(Roles = "2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.GetByIdAsync(id) == null)
            {
                return NotFound(ErrorResponse.HB40401("Resource not found"));
            }

            return Ok(await _service.DeleteAsync(id));
        }

        //[Authorize(Roles = "2")]
        //[HttpGet("Search")]
        //[EnableQuery]
        //public async Task<IEnumerable<BearProfile>> Search(string? BearName, double? weight)
        //{
        //    return await _service.SearchAsync(BearName, weight);
        //}

        [Authorize(Roles = "2,3,4")]
        [HttpGet("Search")]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaging([FromQuery] string? BearName, [FromQuery] double? weight, [FromQuery] int page = 2, [FromQuery] int pageSize = 3)
        {
            return await _service.SearchWithPagingAsync(BearName, weight, page, pageSize);
        }
    }
}
