using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface IMoneyBoxApiService
	{
		Task<MoneyBoxCollectionApiModel> GetMoneyBoxCollectionAsync(long agreementId, CancellationToken cancellationToken);
	}
}