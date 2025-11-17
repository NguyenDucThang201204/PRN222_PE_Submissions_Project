using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repositories.ModelExtentions;
using Services;

namespace PE_PRN232_FA25_LeThienPhuc.api.Controllers
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
        [Authorize(Roles = "1,2,3,4")]
        [HttpGet("BearProfiles")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var items = await _mainService.GetAllAsync();
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
        [Authorize(Roles = "4,1,2,3")]
        [HttpGet("BearProfiles/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return StatusCode(400, new { errorCode = "HB40001", message = "Invalid item ID" });
                }

                var item = await _mainService.GetByIdAsync(id);

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

    }
    }
