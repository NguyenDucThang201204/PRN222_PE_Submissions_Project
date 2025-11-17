using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.ModelExtension;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Models;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BearProfileController : ControllerBase
{
    private readonly IBearProfileService _service;

    public BearProfileController(IBearProfileService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "2,3")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }


    [HttpPost("Search")]
    [Authorize(Roles = "2,3")]
    public async Task<IActionResult> SearchWithPaging([FromBody] BearProfileSearchRequest request)
    {
        var results = await _service.SearchWithPaginationAsync(request);
        return Ok(results);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByKeysAsync(new object[] { id });
        
        if (item == null)
        {
            return NotFound(new ErrorResponse
            {
                ErrorCode = "HB40401",
                Message = "BearProfile not found"
            });
        }

        return Ok(item);
    }

    [HttpPost]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Create([FromBody] BearProfile entity)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "HB40001",
                Message = "Invalid input data"
            });
        }

        await _service.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = 0 }, entity);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Update(string id, [FromBody] BearProfile entity)
    {
        var existing = await _service.GetByKeysAsync(new object[] { id });
        if (existing == null)
        {
            return NotFound(new ErrorResponse
            {
                ErrorCode = "HB40401",
                Message = "BearProfile not found"
            });
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(new ErrorResponse
            {
                ErrorCode = "HB40001",
                Message = "Invalid input data"
            });
        }
        existing.BearName = entity.BearName;
        existing.BearTypeId = entity.BearTypeId;
        existing.BearWeight = entity.BearWeight;
        existing.Characteristics = entity.Characteristics;
        existing.CareNeeds = entity.CareNeeds;
        existing.ModifiedDate = entity.ModifiedDate;

        await _service.UpdateAsync(existing);
        return Ok(existing);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> Delete(int id)
    {
        var existing = await _service.GetByKeysAsync(new object[] { id });
        if (existing == null)
        {
            return NotFound(new ErrorResponse
            {
                ErrorCode = "HB40401",
                Message = "BearProfile not found"
            });
        }

        await _service.DeleteAsync(existing);
        return Ok();
    }
}