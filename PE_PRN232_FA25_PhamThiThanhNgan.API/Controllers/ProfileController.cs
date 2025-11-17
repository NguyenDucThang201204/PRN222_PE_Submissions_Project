using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PE_PRN232_FA25_PhamThiThanhNgan.API.Commons;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.ModelExtensions;
using PE_PRN232_FA25_PhamThiThanhNgan.Repositories.Models;
using PE_PRN232_FA25_PhamThiThanhNgan.Services;
using Practice_FA25_PE_Services.DTO;
using Practice_FA25_PE_Services.Interface;


namespace PE_PRN232_FA25_PhamThiThanhNgan.API.Controllers
{
    [ServiceFilter(typeof(ValidateModelStateFilter))]
    [Route("BearProfiles")]
    [ApiController]
    [Authorize] 
    public class ProfileController : ControllerBase
    {
        private readonly IBearProfileService _profileService;

        public ProfileController(IBearProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        [Authorize(Roles = ConstRoles.FullAccess)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BearProfileDTO[]))]
        public async Task<IActionResult> GetAllProfile()
        {
            var profiles = await _profileService.GetAllProfilesAsync();
            return Ok(profiles);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = ConstRoles.FullAccess)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BearProfileDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))] 
        public async Task<IActionResult> GetById(int id)
        {
            var profileDto = await _profileService.GetProfileByIdAsync(id);
            if (profileDto == null)
            {
                return NotFound(ErrorResponse.ResourceNotFound($"BearProfile with ID {id} not found."));
            }
            return Ok(profileDto);
        }

        [HttpPost("Search")]
        [Authorize(Roles = ConstRoles.FullRead)]
        public async Task<PaginationResult<List<BearProfile>>> SearchWithPaging(BearSearchRequest searchRequest)
        {
            return await _profileService.SearchWithPagingAsync(searchRequest);
        }

        [HttpPost]
        [Authorize(Roles = ConstRoles.FullAccess)]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BearProfileDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateProfile([FromBody] BearProfileRequestDTO profileDto)
        {
            try
            {
                var newProfile = await _profileService.CreateProfileAsync(profileDto);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = newProfile.BearProfileId },
                    newProfile
                );
            }catch (KeyNotFoundException ex)
            {
                return NotFound(ErrorResponse.ResourceNotFound(ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = ConstRoles.FullAccess)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BearProfileDTO))] 
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] BearProfileRequestDTO profileDto)
        {
            try
            {
                var updatedProfile = await _profileService.UpdateProfileAsync(id, profileDto);

                if (updatedProfile == null)
                {
                    return NotFound(ErrorResponse.ResourceNotFound($"BearProfile with ID {id} not found."));
                }

                return Ok(updatedProfile);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ErrorResponse.ResourceNotFound(ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = ConstRoles.FullAccess)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))] 
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var result = await _profileService.DeleteProfileAsync(id);
            if (!result)
            {
                return NotFound(ErrorResponse.ResourceNotFound($"BearProfile with ID {id} not found."));
            }
            return Ok();
        }
    }
}
