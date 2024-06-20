using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class AgreementTariffApiManager
	{
		private readonly IAgreementTariffApiService _agreementTariffApiService;
		public AgreementTariffApiManager(IAgreementTariffApiService agreementTariffApiService)
		{
			_agreementTariffApiService = agreementTariffApiService;
		}

		public async Task<TariffApiModel> GetCurrentTariffAsync(long clientId, long agreementId, CancellationToken cancellationToken)
		{
			return await _agreementTariffApiService.GetCurrentTariffAsync(clientId, agreementId, cancellationToken);
		}
		public async Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long clientId, long agreementId, CancellationToken cancellationToken)
		{
			return await _agreementTariffApiService.GetAllTariffAsync(clientId, agreementId, cancellationToken);
		}

		public async Task<byte[]> GetTariffFileAsync(long clientId, long agreementId, CancellationToken cancellationToken)
		{
			var pathSource = $"{AppContext.BaseDirectory}Files/tarif-BVR.pdf";
			return await FileHelper.GetFileAsync(pathSource);
		}
	}
}
