using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_Service.Interfaces;
using PE_PRN232_FA25_TranNguyenVietQuang_BE.Repository.Entities;
using PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Models;

namespace PE_PRN232_FA25_TranNguyenVietQuang_BE_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BearTypeController : ControllerBase
{
    private readonly IBearTypeService _service;

    public BearTypeController(IBearTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles = "2")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }
}