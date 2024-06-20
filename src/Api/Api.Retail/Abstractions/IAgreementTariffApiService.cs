using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Api.Retail.ApiServices.Abstractions
{
	public interface IAgreementTariffApiService
	{
		Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long? clientId, long agreementId, CancellationToken cancellationToken);
		Task<TariffApiModel> GetCurrentTariffAsync(long? clientId, long agreementId, CancellationToken cancellationToken);
	}
}
