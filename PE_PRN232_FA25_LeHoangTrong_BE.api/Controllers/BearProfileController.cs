using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.ModelExtensions;
using PE_PRN232_FA25_LeHoangTrong_BE_api.Models;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;

namespace PE_PRN232_FA25_LeHoangTrong_BE_api.Controllers;

[ApiController]
[Route("BearProfiles")]
[Authorize]
public class BearProfileController : ControllerBase
{
    private readonly IBearProfileService _service;

    public BearProfileController(IBearProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "1,2,3,4")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("search")]
    [Authorize(Roles = "2,3,4")]
    public async Task<ActionResult<PaginationResult<List<BearProfile>>>> Search([FromBody] BearSearchRequest request)
    {
        var items = await _service.SearchAsync(request);
        return Ok(items);
    }

    [HttpGet("{id}")]
    [Authorize()]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByKeysAsync(new object[] { id });

        if (item == null)
        {
            return NotFound(new ErrorResponse
            {
                ErrorCode = "404",
                Message = "BearProfile not found"
            });
        }

        return Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Create([FromBody] BearProfile entity)
    {
        entity.BearProfileId = 0;

        //if (!ModelState.IsValid)
        //{
        //    return BadRequest(new ErrorResponse
        //    {
        //        ErrorCode = "400",
        //        Message = "Invalid input data"
        //    });
        //}

        try
        {
            await _service.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = 0 }, entity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                ErrorCode = "500",
                Message = $"Error creating BearProfile: {ex.Message}"
            });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Update(int id, [FromBody] BearProfile entity)
    {
        try
        {
            var existing = await _service.GetByKeysAsync(new object[] { id });
            if (existing == null)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = "404",
                    Message = "BearProfile not found"
                });
            }

            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(new ErrorResponse
            //    {
            //        ErrorCode = "400",
            //        Message = "Invalid input data"
            //    });
            //}

            await _service.UpdateAsync(entity);
            return Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                ErrorCode = "500",
                Message = $"Error updating BearProfile: {ex.Message}"
            });
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var existing = await _service.GetByKeysAsync(new object[] { id });
            if (existing == null)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = "404",
                    Message = "BearProfile not found"
                });
            }

            await _service.DeleteAsync(existing);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse
            {
                ErrorCode = "500",
                Message = $"Error deleting BearProfile: {ex.Message}"
            });
        }
    }
}