using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Core.Common.Enums.Cards;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface ISmsInfoApiService
	{
		Task<SmsInfoApiModel> GetStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken);
		Task<SmsInfoChangeStatusApiModel> ChangeStatusAsync(long? clientId, long cardId, SmsInfoChangeStatusEnum status, CancellationToken cancellationToken);
		Task<SmsInfoHistoryStatusApiModel> HistoryStatusAsync(long? clientId, long cardId, CancellationToken cancellationToken);

	}
}
