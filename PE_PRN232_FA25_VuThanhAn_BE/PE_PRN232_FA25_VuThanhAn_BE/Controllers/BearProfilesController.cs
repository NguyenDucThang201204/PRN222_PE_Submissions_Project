using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository.Models;
using Service;

namespace PE_PRN232_FA25_VuThanhAn_BE.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class BearProfilesController : ControllerBase
	{
		private readonly IBearProfileService _bearProfileService;
		public BearProfilesController(IBearProfileService bearProfileService)
		{
			_bearProfileService = bearProfileService;
		}

		[HttpGet]
		[Authorize(Roles = "1, 2, 3, 4")]
		public async Task<ActionResult<ErrorCodeModel>> GetAll()
		{
			try
			{
				var lionProfiles = await _bearProfileService.GetAllAsync();

				if (lionProfiles == null || lionProfiles.Count == 0)
				{
					return StatusCode(404, ErrorCodeModel.NotFound());
				}
				return Ok(lionProfiles);
			}
			catch
			{
				return StatusCode(500, ErrorCodeModel.InternalException());
			}
		}

		//[HttpGet("search")]
		//[Authorize(Roles = "1, 2, 3, 4")]
		//[EnableQuery]
		//public async Task<ActionResult<ErrorCodeModel>> Search()
		//{
		//	try
		//	{
		//		var lionProfiles = await _bearProfileService.GetAllAsync();

		//		if (lionProfiles == null || lionProfiles.Count == 0)
		//		{
		//			return StatusCode(404, ErrorCodeModel.NotFound());
		//		}
		//		return Ok(lionProfiles);
		//	}
		//	catch
		//	{
		//		return StatusCode(500, ErrorCodeModel.InternalException());
		//	}
		//}

		[HttpGet("{id}")]
		[Authorize(Roles = "1, 2, 3, 4")]
		public async Task<ActionResult<ErrorCodeModel>> GetById(int id)
		{
			try
			{
				var lionProfile = await _bearProfileService.GetByIdAsync(id);
				if (lionProfile == null)
				{
					return StatusCode(404, ErrorCodeModel.NotFound());
				}
				return Ok(lionProfile);
			}
			catch
			{
				return StatusCode(500, ErrorCodeModel.InternalException());
			}
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "1, 2")]
		public async Task<ActionResult<ErrorCodeModel>> Delete(int id)
		{
			try
			{
				var result = await _bearProfileService.DeleteLAsync(id);
				if (!result)
				{
					return StatusCode(404, ErrorCodeModel.NotFound());
				}
				return Ok($"Deleted BearProfile with ID({id}) successfully!");
			}
			catch
			{
				return StatusCode(500, ErrorCodeModel.InternalException());
			}
		}

		[HttpPost]
		[Authorize(Roles = "1, 2")]
		public async Task<ActionResult<ErrorCodeModel>> Create([FromBody] BearProfile @object)
		{
			try
			{
				if (@object == null)
				{
					return StatusCode(400, ErrorCodeModel.Invalid());
				}

				var result = await _bearProfileService.CreateAsync(@object);

				if (result <= 0)
					return StatusCode(500, ErrorCodeModel.InternalException());

				return Ok(result);
			}
			catch
			{
				return StatusCode(500, ErrorCodeModel.InternalException());
			}
		}

		[HttpPut]
		[Authorize(Roles = "1, 2")]
		public async Task<ActionResult<ErrorCodeModel>> Update([FromBody] BearProfile @object)
		{
			try
			{
				if (@object == null)
				{
					return StatusCode(400, ErrorCodeModel.Invalid());
				}
				var result = await _bearProfileService.UpdateAsync(@object);
				if (result <= 0)
					return StatusCode(500, ErrorCodeModel.InternalException());
				return Ok(result);
			}
			catch
			{
				return StatusCode(500, ErrorCodeModel.InternalException());
			}
		}

	}
}
