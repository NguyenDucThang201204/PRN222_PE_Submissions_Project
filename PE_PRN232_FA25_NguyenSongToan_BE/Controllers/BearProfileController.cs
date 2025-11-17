using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Models;
using Repository.ModelExtensions;
using Services;
using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_NguyenSongToan_BE.Controllers
{

    public record CreateRequest
(
   [Required(ErrorMessage = "BearName is required")]
        [RegularExpression(@"^([A-Za-z0-9]+(?:\s+[A-Za-z0-9]+)*)$",
            ErrorMessage = "BearName must contain only letters/numbers, separated by spaces")]
        [MaxLength(50)]
        [MinLength(4)]
        string BearName,

   [Required(ErrorMessage = "Characteristics is required")]
        string Characteristics,

      [Required(ErrorMessage = "CareNeeds is required")]
        string CareNeeds,

   [Required(ErrorMessage = "BearWeight is required")]
        [Range(200, double.MaxValue, ErrorMessage = "BearWeight must be greater than 200")]
        int BearWeight,

   [Required(ErrorMessage = "BearTypeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "BearTypeId must be greater than 0")]
        int BearTypeId
);

    public record UpdateRequest
(
[Required(ErrorMessage = "BearName is required")]
        [RegularExpression(@"^([A-Za-z0-9]+(?:\s+[A-Za-z0-9]+)*)$",
            ErrorMessage = "BearName must contain only letters/numbers, separated by spaces")]
        string BearName,

[Required(ErrorMessage = "Characteristics is required")]
        string Characteristics,

   [Required(ErrorMessage = "CareNeeds is required")]
        string CareNeeds,

[Required(ErrorMessage = "BearWeight is required")]
        [Range(0.00000000000001, double.MaxValue, ErrorMessage = "BearWeight must be greater than 0")]
        int BearWeight,

[Required(ErrorMessage = "BearTypeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "BearTypeId must be greater than 0")]
        int BearTypeId,

[Required(ErrorMessage = "ModifiedDate is required")]
DateOnly ModifiedDate
);


    [Route("BearProfiles")]
    [ApiController]
    [Authorize]
    public class BearProfileController : Controller
    {
        private readonly BearProfileService _service;

        public BearProfileController(BearProfileService service)
        {
            _service = service;
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

        [HttpPost]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> Create([FromBody] CreateRequest request)
        {
            var entity = new BearProfile
            {
                BearName = request.BearName,
                Characteristics = request.Characteristics,
                CareNeeds = request.CareNeeds,
                BearWeight = request.BearWeight,
                BearTypeId = request.BearTypeId
            };

            var newId = await _service.CreateAsync(entity);

            entity = await _service.GetByIdAsync(entity.BearProfileId);

            return CreatedAtAction(nameof(GetById), new { id = entity.BearProfileId }, entity);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "1, 2")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateRequest request)
        {
            var existingEntity = await _service.GetByIdAsync(id);

            if (existingEntity == null)
                throw new KeyNotFoundException($"ID {id} not found");

            existingEntity.BearName = request.BearName;
            existingEntity.Characteristics = request.Characteristics;
            existingEntity.CareNeeds = request.CareNeeds;
            existingEntity.BearWeight = request.BearWeight;
            existingEntity.BearTypeId = request.BearTypeId;
            existingEntity.ModifiedDate = request.ModifiedDate  ;

            await _service.UpdateAsync(existingEntity);

            var updatedEntity = await _service.GetByIdAsync(id);

            return Ok(updatedEntity);
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

        [Authorize(Roles = "1, 2")]
        [HttpGet("Search")]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaging([FromQuery] MainSearchRequest request)
        {
            return await _service.SearchWithPaginationAsync(request);
        }
    }
}
