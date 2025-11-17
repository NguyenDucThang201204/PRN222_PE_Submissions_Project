using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service;
using Service.Model;

namespace PE_PRN232_FA25_HoangMinhDai_BE.Controllers
{
    [Route("api/BearProfiles")]
    [ApiController]
    public class BearProfilesController : ControllerBase
    {
        private readonly IItemService _itemService;
        public BearProfilesController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<IEnumerable<BearProfile>>> GetAll()
        {
            try
            {
                var result = await _itemService.GetAll();
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _itemService.GetByIdAsync(id);
                if (item == null)
                    return NotFound(new { errorCode = "HB40401", message = "Resource not found" });

                return Ok(item);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<ActionResult<BearProfile>> Add([FromBody] ItemEditModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorCode = "HB40001", message = ModelState });
                }

                var newItem = new BearProfile
                {
                    BearProfileId = item.BearProfileId,
                    BearTypeId = item.BearTypeId,
                    BearName = item.BearName,
                    Weight = (Double)item.Weight,
                    Characteristics = item.Characteristics,
                    CareNeeds = item.CareNeeds,
                    ModifiedDate = DateTime.Now
                };

                var result = await _itemService.AddAsync(newItem);
                return StatusCode(201, result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorCode = "HB40001", message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Update(int id, [FromBody] ItemEditModel item)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { errorCode = "HB40001", message = ModelState });
                }

                if (id != item.BearProfileId)
                {
                    return BadRequest(new { errorCode = "HB40001", message = "Id mismatch" });
                }

                var existingItem = await _itemService.GetByIdAsync(id);
                if (existingItem == null)
                {
                    return NotFound(new { errorCode = "HB40401", message = "Resource not found" });
                }

                existingItem.BearProfileId = item.BearProfileId;
                existingItem.BearTypeId = item.BearTypeId;
                existingItem.BearName = item.BearName;
                existingItem.Weight = (Double)item.Weight;
                existingItem.Characteristics = item.Characteristics;
                existingItem.CareNeeds = item.CareNeeds;
                existingItem.ModifiedDate = DateTime.Now;

                var result = await _itemService.UpdateAsynce(existingItem);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { errorCode = "HB40001", message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "2")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingItem = await _itemService.GetByIdAsync(id);
                if (existingItem == null)
                {
                    return NotFound(new { errorCode = "HB40401", message = "Resource not found" });
                }

                var result = await _itemService.DeleteAsync(id);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }

        [HttpGet("Search")]
        [Authorize(Roles = "2,3,4")]
        public async Task<ActionResult<IEnumerable<BearProfile>>> Search([FromQuery] string? BearName, [FromQuery] double? BearWeight, [FromQuery] string? BearTypeName)
        {
            try
            {
                var result = await _itemService.Search(BearName, BearWeight, BearTypeName);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(401, new { errorCode = "HB40101", message = "Token missing/invalid" });
            }
            catch (InvalidOperationException)
            {
                return StatusCode(403, new { errorCode = "HB40301", message = "Permission denied" });
            }
            catch (Exception)
            {
                return StatusCode(500, new { errorCode = "HB50001", message = "Internal server error" });
            }
        }
    }
}
