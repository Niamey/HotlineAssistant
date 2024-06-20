using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models.Cards;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class Secure3DApiManager
	{
		private readonly ISecure3DApiService _secure3DApiService;
		public Secure3DApiManager(ISecure3DApiService secure3DApiService)
		{
			_secure3DApiService = secure3DApiService;
		}

		public async Task<Secure3DInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _secure3DApiService.GetStatusAsync(clientId, cardId, cancellationToken);
		}

		public async Task<Secure3DStatusHistoryApiModel> GetStatusHistoryAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _secure3DApiService.GetStatusHistoryAsync(clientId, cardId, cancellationToken);
		}
	}
}

