using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_DaoTrongTien_BE.API.DTOs;
using PE_PRN232_FA25_DaoTrongTien_BE.BLL.Services;
using PE_PRN232_FA25_DaoTrongTien_BE.DAL.Models;

namespace PE_PRN232_FA25_DaoTrongTien_BE.API.Controllers
{
        [Route("api/")]
        [ApiController]
        public class BearProfileController : ControllerBase
        {
            private readonly IBearProfileService _service;
            public BearProfileController(IBearProfileService service)
            {
                _service = service;
            }
            [Authorize(Roles = "1,2")]
            [HttpGet("BearProfiles")]
            public async Task<List<BearProfile>> GetAllBearProfiles()
            {
                return await _service.GetAllBearProfiles();
            }
        [Authorize(Roles = "1,2")]
        [HttpGet("BearProfiles/{id}")]
        public async Task<BearProfile> GetBearProfileById(int id)
        {
            var response = await _service.GetBearProfileById(id);
            return response;
        }

        [Authorize(Roles = "1,2")]
        [HttpPost("BearProfiles")]
            public async Task<IActionResult> AddBearProfile(CreateBearProfileRequest request)
            {
                if (request == null)
                {
                    return BadRequest(new ErrorResponse
                    {
                        message = "BearProfile data is required."
                    });
                }

                var BearProfile = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearTypeId = request.BearTypeId,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate
                };

                await _service.AddBearProfile(BearProfile);
                return Ok(new { message = "BearProfile added successfully." });
            }
        [Authorize(Roles = "1,2")]
        [HttpPut("BearProfiles/{id}")]
            public async Task<IActionResult> UpdateBearProfile(int id, CreateBearProfileRequest request)
            {
                if (request == null || id != request.BearProfileId)
                {
                    return BadRequest(new ErrorResponse
                    {
                        message = "Invalid BearProfile data."
                    });
                }

                var BearProfile = new BearProfile
                {
                    BearProfileId = request.BearProfileId,
                    BearTypeId = request.BearTypeId,
                    BearName = request.BearName,
                    BearWeight = request.BearWeight,
                    Characteristics = request.Characteristics,
                    CareNeeds = request.CareNeeds,
                    ModifiedDate = request.ModifiedDate
                };

                await _service.UpdateBearProfile(BearProfile);
                return Ok(new { message = "BearProfile updated successfully." });
            }

        [Authorize(Roles = "1,2")]
        [HttpDelete("BearProfiles/{id}")]
            public async Task<IActionResult> DeleteBearProfile(int id)
            {
                await _service.DeleteBearProfile(id);
                return Ok();
            }
        }

    }

