using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.ModelExtensions;
using Repositories.Models;
using Services;
using Services.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PE_PRN232_FA25_LeQuangThaiSon_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly BearProfileService _service;
        private readonly BearTypeService _serviceType;

        public BearProfilesController(BearProfileService service, BearTypeService serviceType)
        {
            _service = service;
            _serviceType = serviceType;
        }

        [Authorize(Roles = "2")]
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
                return NotFound();
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
                return BadRequest();
            }

            if (await _serviceType.GetByIdAsync(request.BearTypeId) == null)
            {
                return BadRequest();
            }

            var item = new BearProfile()
            {
                BearName = request.BearName,
                CareNeeds = request.CareNeeds,
                Characteristics = request.Characteristics,
                BearTypeId = request.BearTypeId,
                BearWeight = request.BearWeight,
                ModifiedDate = request.ModifiedDate
            };
            return Ok(await _service.CreateAsync(item));
        }

        [Authorize(Roles = "2")]
        [HttpPut]
        public async Task<IActionResult> Put(BearProfileUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var error = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault()?.ErrorMessage ?? "Missing/Invalid input";
                return BadRequest();
            }

            var existing = await _service.GetByIdAsync(request.BearProfileId);

            if (existing == null)
            {
                return NotFound();
            }

            if (await _serviceType.GetByIdAsync(request.BearTypeId) == null)
            {
                return BadRequest();
            }

            existing.BearName = request.BearName;
            existing.CareNeeds = request.CareNeeds;
            existing.BearWeight = request.BearWeight;
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
                return NotFound();
            }

            return Ok(await _service.DeleteAsync(id));
        }


        [Authorize(Roles = "2,3,4")]
        [HttpGet("Search")]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaging(string? bearName,  double? bearWeight, string? bearTypeName,  int page = 1,  int pageSize = 10)
        {
            return await _service.SearchWithPagingAsync(bearName, bearWeight, bearTypeName, page, pageSize);
        }
    }
}
