using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.ModelExtensions;
using Repositories.Models;
using Services;

namespace PE_PRN232_FA25_NguyenMinhQuang_BE.api.Controllers
{
    [Authorize]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly MainService _mainService;

        public MainController(MainService mainService)
        {
            _mainService = mainService;
        }

        [EnableQuery]
        [Authorize(Roles = "2")]
        [HttpGet("BearProfiles")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var items = await _mainService.GetAllAsync();
                if (items == null)
                {
                    return NotFound(new { errorCode = "HB40401", message = "No Animal found" });
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        // GET api/BearProfile/5
        [EnableQuery]
        [Authorize(Roles = "2")]
        [HttpGet("BearProfiles/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new { errorCode = "HB40001", message = "Invalid Animal Id" });
                }

                var item = await _mainService.GetByIdAsync(id);

                if (item == null || item.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Animal with Id {id} not found" });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        // POST api/BearProfile
        [Authorize(Roles = "2")]
        [HttpPost("BearProfiles")]
        public async Task<IActionResult> Post([FromBody] CreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var firstError = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .FirstOrDefault();

                    return BadRequest(new
                    {
                        errorCode = "HB40001",
                        message = firstError?.ErrorMessage ?? "Invalid input data"
                    });
                }

                var item = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate,
                    BearTypeId = request.BearTypeId
                };

                var result = await _mainService.CreateAsync(item);

                if (result > 0)
                {
                    return StatusCode(201, new { message = "Animal created successfully", id = result });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to insert new Animal" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        // PUT api/BearProfile/5
        [Authorize(Roles = "2")]
        [HttpPut("BearProfiles/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateRequest request)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Invalid ID" });
                }

                if (!ModelState.IsValid)
                {
                    var firstError = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .FirstOrDefault();

                    return BadRequest(new
                    {
                        errorCode = "HB40001",
                        message = firstError?.ErrorMessage ?? "Invalid input data"
                    });
                }

                var existingItem = await _mainService.GetByIdAsync(id);
                if (existingItem == null || existingItem.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Animal with Id {id} not found" });
                }

                var item = new BearProfile
                {
                    BearProfileId = id,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate,
                    BearTypeId = request.BearTypeId,

                };

                var result = await _mainService.UpdateAsync(item);

                if (result > 0)
                {
                    return Ok(new { message = "Item updated successfully", affectedRows = result });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to update Animal" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        // DELETE api/BearProfile/5
        [Authorize(Roles = "2")]
        [HttpDelete("BearProfiles/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Invalid ID" });
                }

                var existingItem = await _mainService.GetByIdAsync(id);
                if (existingItem == null || existingItem.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Animal with Id {id} not found" });
                }

                var result = await _mainService.DeleteAsync(id);

                if (result)
                {
                    return Ok(new { message = "Item deleted successfully" });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to delete Animal" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }


        [Authorize(Roles = "2,3,4")]
        [HttpPost("BearProfiles/Search")]
        public async Task<IActionResult> SearchWithPaging([FromBody] MainSearchRequest item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Search request is required" });
                }

                if (item.CurrentPage == null || item.CurrentPage <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "currentPage must be greater than 0" });
                }

                var result = await _mainService.SearchPagingAsync(item);

                var response = new SearchResponse
                {
                    TotalItems = result.TotalItems,
                    TotalPages = result.TotalPages,
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize,
                    Items = result.Items ?? new List<BearProfile>()
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred while searching with paging" });
            }
        }

        public class SearchResponse
        {
            public int TotalItems { get; set; }
            public int TotalPages { get; set; }
            public int CurrentPage { get; set; }
            public int PageSize { get; set; }
            public List<BearProfile> Items { get; set; }
        }

    }
}
