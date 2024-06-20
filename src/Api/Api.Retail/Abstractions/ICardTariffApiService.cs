using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface ICardTariffApiService
	{
		Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long? clientId, long cardId, CancellationToken cancellationToken);
		Task<TariffApiModel> GetCurrentTariffAsync(long? clientId, long cardId, CancellationToken cancellationToken);
	}
}
