using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_LeHoangTrong_BE_service.Interfaces;
using PE_PRN232_FA25_LeHoangTrong_BE.repo.Entities;
using PE_PRN232_FA25_LeHoangTrong_BE_api.Models;

namespace PE_PRN232_FA25_LeHoangTrong_BE_api.Controllers;

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
    [Authorize(Roles = "administrator,moderator,developer,member")]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items);
    }
}