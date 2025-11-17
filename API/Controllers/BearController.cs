using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.DTO;
using Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers;
[Route("BearProfiles")]
[ApiController]
[Authorize]
public class BearController : ControllerBase
{

    private readonly BearService _bearService;

    public BearController(BearService bearService)
    {
        _bearService = bearService;
    }
    // GET: api/<BearController>
    [Authorize(Roles = "2,3,4")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _bearService.GetAllBearProfileAsync();
        return Ok(items);
    }

    // GET api/<BearController>/5
    [Authorize(Roles = "2,3,4")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _bearService.GetBearProfileByIdAsync(id);
        return Ok(item);
    }

    // POST api/<BearController>
    [Authorize(Roles = "2")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRequest createRequest)
    {
        var item = await _bearService.CreateBearProfileAsync(createRequest);
        return Ok(item);
    }

    // PUT api/<BearController>/5
    [Authorize(Roles = "2")]
    [HttpPut]
    public async Task<IActionResult> Put([FromBody] UpdateRequest updateRequest)
    {
        var item = await _bearService.UpdateBearProfileAsync(updateRequest.Id, updateRequest);
        return Ok(item);
    }

    // DELETE api/<BearController>/5
    [Authorize(Roles = "2")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _bearService.DeleteBearProfileAsync(id);
        return Ok(item);
    }

}
