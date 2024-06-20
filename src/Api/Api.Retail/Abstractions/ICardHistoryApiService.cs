using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models.Cards;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface ICardHistoryApiService
	{
		Task<CardCollectionHistoryStatusApiModel> GetCardHistoryStatusAsync(long cardId, CancellationToken cancellationToken);
	}
}