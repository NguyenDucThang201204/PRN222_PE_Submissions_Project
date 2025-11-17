using Repository;
using Repository.Models;

namespace Service
{
	public interface IBearAccountService
	{
		Task<BearAccount?> GetBearAccountAsync(string userName, string password);
	}
	public class BearAccountService : IBearAccountService
	{
		private readonly BearAccountRepository _bearAccountRepository;
		public BearAccountService()
		{
			_bearAccountRepository = new BearAccountRepository();
		}

		public BearAccountService(BearAccountRepository bearAccountRepository)
		{
			_bearAccountRepository = bearAccountRepository;
		}


		public async Task<BearAccount?> GetBearAccountAsync(string userName, string password)
		{
			return await _bearAccountRepository.GetUserAccountAsync(userName, password);
		}
	}
}
