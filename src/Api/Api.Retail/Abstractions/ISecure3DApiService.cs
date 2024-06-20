using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface ISecure3DApiService
	{
		Task<Secure3DInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken);
		Task<Secure3DStatusHistoryApiModel> GetStatusHistoryAsync(long? clientId, long cardId, CancellationToken cancellationToken);
	}
}
