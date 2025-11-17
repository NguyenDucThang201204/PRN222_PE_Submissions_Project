using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.ModelExtensions;
using Repositories.Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PE_PRN232_FA25_PhanQuoiAnPhu.API.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        private readonly string ERROR_400 = "Missing/invalid input";
        private readonly string ERROR_404 = "Resource not found";
        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetAllAsyncIncludeOrderBy();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _service.GetByIdAsyncInclude(id);
            if (result == null)
                return NotFound(ERROR_404); // 404
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "2")] // Replace with authorized roles
        public async Task<IActionResult> Create([FromBody] BearProfile entity)
        {
            try
            {
                if (entity == null)
                {
                    return BadRequest(ERROR_400);
                }

                var maxId = await _service.GetMaxId();
                entity.BearProfileId = maxId + 1;
                entity.BearType = null;

                var created = await _service.CreateAsync(entity);
                var result = await _service.GetByIdAsyncInclude(created.BearProfileId);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(ERROR_400); // 400
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "2")] // Replace with authorized roles
        public async Task<IActionResult> Update(int id, [FromBody] BearProfile updatedEntity)
        {
            try
            {
                var existingEntity = await _service.GetByIdAsyncInclude(id);
                if (existingEntity == null)
                    return NotFound(ERROR_404); // 404

                /*
                existingEntity.ModelName = updatedEntity.ModelName ?? existingEntity.ModelName;
                existingEntity.Material = updatedEntity.Material ?? existingEntity.Material;
                existingEntity.Color = updatedEntity.Color ?? existingEntity.Color;
                existingEntity.Price = updatedEntity.Price ?? existingEntity.Price;
                existingEntity.Stock = updatedEntity.Stock ?? existingEntity.Stock;
                existingEntity.ReleaseDate = updatedEntity.ReleaseDate ?? existingEntity.ReleaseDate;
                existingEntity.BrandId = updatedEntity.BrandId ?? existingEntity.BrandId;
                existingEntity.Brand = null;
                */

                var updated = await _service.UpdateAsync(existingEntity);
                var result = await _service.GetByIdAsyncInclude(updated.BearProfileId);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(ERROR_400); // 400
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")] // Replace with authorized roles
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var exist = await _service.GetByIdAsyncInclude(id);
                if (exist == null)
                    return NotFound(ERROR_404); // 404
                var result = await _service.DeleteAsync(id);
                if (result)
                    return Ok();
                else
                    return NotFound(ERROR_404); // 404
            }
            catch (Exception)
            {
                return BadRequest(ERROR_400); // 400
            }
        }

        [HttpPost("Search")]
        [Authorize(Roles = "2,3,4")]
        public async Task<IActionResult> Search(
            [FromQuery] string? bearName,
            [FromQuery] string? bearWeight,
            [FromQuery] string? bearTypeName,
            [FromQuery] int currentPage = 2,
            [FromQuery] int pageSize = 3)
        {
            try
            {
                // Validate pagination parameters
                if (currentPage < 1 || pageSize < 1)
                {
                    return BadRequest(ERROR_400);
                }

                if (string.IsNullOrWhiteSpace(bearName) && string.IsNullOrWhiteSpace(bearTypeName))
                {
                    var allResults = await _service.GetAllAsyncIncludeOrderBy();

                    var totalItems = allResults.Count;
                    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                    var pagedItems = allResults
                        .Skip((currentPage - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    var paginationResult = new PaginationResult<BearProfile>
                    {
                        TotalItems = totalItems,
                        TotalPages = totalPages,
                        CurrentPage = currentPage,
                        PageSize = pageSize,
                        Items = pagedItems
                    };

                    return Ok(paginationResult);
                }

                // Example: 2 Text Search, OR, include subtable, ascending, with paging
                var searchResult = await _service.SearchWithPagingAsyncIncludeOrderBy(
                    predicate: result => (
                        !string.IsNullOrWhiteSpace(bearName) &&
                        result.BearName != null &&
                        result.BearName.Contains(bearName)
                    ) || (
                        !string.IsNullOrWhiteSpace(bearTypeName) &&
                        result.Characteristics != null &&
                        result.Characteristics.Contains(bearTypeName)
                    ),
                    currentPage: currentPage,
                    pageSize: pageSize);

                return Ok(searchResult);
            }
            catch (Exception)
            {
                return BadRequest(ERROR_400);
            }
        }
    }
}