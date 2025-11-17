using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repos.ModelExtensions;
using Repos.Models;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PE_PRN232_FA25_PhamHoTanDat_BE.Controllers
{
    [Route("BearProfiles")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly MainService _service;

        public BearProfilesController(MainService service)
        {
            _service = service;
        }

        [EnableQuery]
        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var items = await _service.GetAllAsync();
                if (items == null)
                {
                    return NotFound(new { errorCode = "HB40401", message = "No Item found" });
                }
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }


        [EnableQuery]
        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new { errorCode = "HB40001", message = "Invalid item ID" });
                }

                var item = await _service.GetByIdAsync(id);

                if (item == null || item.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Item with ID {id} not found" });
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }



        [Authorize(Roles = "1, 2")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var first = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .FirstOrDefault();

                    return BadRequest(new
                    {
                        errorCode = "HB40001",
                        message = first?.ErrorMessage ?? "Invalid input data"
                    });
                }

                var item = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearTypeId = request.BearTypeId,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate,

                };

                var result = await _service.CreateAsync(item);

                if (result > 0)
                {
                    return StatusCode(201, new { message = "Item created successfully" });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to create item" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        [Authorize(Roles = "1, 2")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateRequest request)
        {
            try
            {
                if (request.BearProfileId <= 0)
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

                var existingItem = await _service.GetByIdAsync(request.BearProfileId);
                if (existingItem == null || existingItem.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Item with ID {request.BearProfileId} not found" });
                }

                var item = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearTypeId = request.BearTypeId,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate,

                };

                var result = await _service.UpdateAsync(item);

                if (result > 0)
                {
                    return Ok(new { message = "Item updated successfully" });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to update item" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }


        [Authorize(Roles = "1, 2")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Invalid ID" });
                }

                var existingItem = await _service.GetByIdAsync(id);
                if (existingItem == null || existingItem.BearProfileId == 0)
                {
                    return NotFound(new { errorCode = "HB40401", message = $"Item with ID {id} not found" });
                }

                var result = await _service.DeleteAsync(id);

                if (result)
                {
                    return Ok(new { message = "Item deleted successfully" });
                }

                return StatusCode(500, new { errorCode = "HB50001", message = "Failed to delete item" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred" });
            }
        }

        //[Authorize(Roles = "1, 2, 3, 4")]
        //[HttpGet("search")]
        //[EnableQuery]
        //public async Task<IActionResult> Search([FromQuery] string? bearName, [FromQuery] decimal? bearWeight, [FromQuery] string? bearTypeName)
        //{
        //    try
        //    {
        //        var items = await _service.SearchAsync(bearName, bearWeight, bearTypeName);



        //        return Ok(items);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred while searching n" });
        //    }
        //}

        [Authorize(Roles = "1, 2, 3, 4")]
        [HttpPost("Search")]
        public async Task<IActionResult> SearchWithPaging([FromBody] MainSearchRequest searchRequest)
        {
            try
            {
                if (searchRequest == null)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Search request is required" });
                }

                if (searchRequest.CurrentPage == null || searchRequest.CurrentPage <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "currentPage must be greater than 0" });
                }

                if (searchRequest.PageSize == null || searchRequest.PageSize <= 0)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "pageSize must be greater than 0" });
                }

                var result = await _service.SearchWithPaginationAsync(searchRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error occurred while searching with paging" });
            }
        }
    }
}
