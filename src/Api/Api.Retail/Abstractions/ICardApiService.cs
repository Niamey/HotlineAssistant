using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface ICardApiService
	{
		Task<CardBalanceApiModel> GetCardBalanceAsync(long? clientId, long cardId, CancellationToken cancellationToken);
		Task<CardCollectionApiModel> GetCardCollectionAsync(long clientId, CancellationToken cancellationToken);
		Task<CardApiModel> GetCardAsync(long? clientId, long cardId, CancellationToken cancellationToken);
		Task<CardChangeStatusResultApiModel> ChangeStatusAsync(long cardId, CardStatusRetailEnum status, string comment, CancellationToken cancellationToken);
	}
}