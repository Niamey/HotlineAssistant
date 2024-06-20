using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Models;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class MoneyBoxApiManager
	{
		private readonly IMoneyBoxApiService _moneyBoxApiService;
		public MoneyBoxApiManager(IMoneyBoxApiService moneyBoxApiService)
		{
			_moneyBoxApiService = moneyBoxApiService;
		}

		public async Task<MoneyBoxCollectionApiModel> GetMoneyBoxCollectionAsync(long agreementId, CancellationToken cancellationToken)
		{
			var result = await _moneyBoxApiService.GetMoneyBoxCollectionAsync(agreementId, cancellationToken);

			return result;
		}
	}
}
