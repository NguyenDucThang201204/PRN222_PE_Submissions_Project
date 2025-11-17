using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
	public interface IBearProfileService
	{
		Task<List<BearProfile>> GetAllAsync();
		Task<BearProfile?> GetByIdAsync(int id);
		Task<int> CreateAsync(BearProfile @object);
		Task<int> UpdateAsync(BearProfile @object);
		Task<bool> DeleteLAsync(int id);

	}
	public class BearProfileService : IBearProfileService
	{
		private readonly BearProfileRepository _bearProfileRepository;

		public BearProfileService()
		{
			_bearProfileRepository = new BearProfileRepository();
		}

		public BearProfileService(BearProfileRepository bearProfileRepository)
		{
			_bearProfileRepository = bearProfileRepository;
		}

		public async Task<int> CreateAsync(BearProfile @object)
		{
			if (@object == null)
			{
				throw new ArgumentNullException(nameof(@object), "BearProfile cannot be null");
			}

			@object.ModifiedDate = DateTime.UtcNow;

			return await _bearProfileRepository.CreateAsync(@object);

		}

		public async Task<bool> DeleteLAsync(int id)
		{
			var bearProfile = await _bearProfileRepository.GetByIdAsync(id);

			if (bearProfile == null)
			{
				return false;
			}

			return await _bearProfileRepository.RemoveAsync(bearProfile);
		}

		public async Task<List<BearProfile>> GetAllAsync()
		{
			return await _bearProfileRepository.GetAllAsync();
		}

		public async Task<BearProfile?> GetByIdAsync(int id)
		{
			return await _bearProfileRepository.GetByIdAsync(id);
		}

		public async Task<int> UpdateAsync(BearProfile @object)
		{
			if (@object == null)
			{
				throw new ArgumentNullException(nameof(@object), "BearProfile cannot be null");
			}

			@object.ModifiedDate = DateTime.UtcNow;

			var existingLionProfile = await _bearProfileRepository.GetByIdAsync(@object.BearProfileId);

			if (existingLionProfile == null)
			{
				throw new InvalidOperationException($"BearProfile with ID {@object.BearProfileId} does not exist.");
			}

			return await _bearProfileRepository.UpdateAsync(@object);

		}
	}
}
