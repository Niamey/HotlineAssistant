using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Models;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class AccountApiManager
	{
		private readonly IAccountApiService _accountApiService;
		public AccountApiManager(IAccountApiService accountApiService)
		{
			_accountApiService = accountApiService;
		}

		public async Task<AccountCollectionApiModel> GetAccountCollectionAsync(long clientId, CancellationToken cancellationToken)
		{
			var result = await _accountApiService.GetAccountCollectionAsync(clientId, cancellationToken);
			if (result != null)
				return new AccountCollectionApiModel(result.Where(x => x.SolarId == clientId));

			return result;
		}
	}
}
