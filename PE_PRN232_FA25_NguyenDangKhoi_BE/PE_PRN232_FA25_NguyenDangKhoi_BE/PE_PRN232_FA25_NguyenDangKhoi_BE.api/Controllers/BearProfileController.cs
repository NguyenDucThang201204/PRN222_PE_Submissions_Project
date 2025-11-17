using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.Models;
using Repositories.ModelExtensions;
using Services;
using System.ComponentModel.DataAnnotations;

namespace PE_PRN232_FA25_NguyenDangKhoi.api.Controllers
{
    public record CreateRequest
    (
        [Required(ErrorMessage = "bearName is required")]
        [Range(4, 50, ErrorMessage = "bearName must be between 4 and 50 characters")]
        [RegularExpression(@"^([A-Za-z0-9]+(?:\s+[A-Za-z0-9]+)*)$",
            ErrorMessage = "bearName must contain only letters/numbers, separated by spaces")]
        string BearName,

        [Required(ErrorMessage = "Characteristics is required")]
        string Characteristics,

        [Required(ErrorMessage = "BearWeight is required")]
        [Range(201, int.MaxValue, ErrorMessage = "BearWeight must be greater than 200")]
        int BearWeight,

        [Required(ErrorMessage = "TypeId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "brandId must be greater than 0")]
        int BearTypeId
    );

  

    [Route("BearProfiles")]
    [ApiController]
    [Authorize]
    public class BearProfileController : ControllerBase
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
                BearWeight = request.BearWeight,
                BearTypeId = request.BearTypeId
            };

            var newId = await _service.CreateAsync(entity);

            entity = await _service.GetByIdAsync(entity.BearProfileId);

            return CreatedAtAction(nameof(GetById), new { id = entity.BearProfileId }, entity);
        }

        



    }
}
