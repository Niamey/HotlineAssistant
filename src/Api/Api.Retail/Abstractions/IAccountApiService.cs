using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models;

namespace Vostok.Hotline.Api.Retail.Abstractions
{
	public interface IAccountApiService
	{
		Task<AccountCollectionApiModel> GetAccountCollectionAsync(long clientId, CancellationToken cancellationToken);
	}
}