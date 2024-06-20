using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Retail.Managers
{
	public class CardTariffApiManager
	{
		private readonly ICardTariffApiService _cardTariffApiService;
		public CardTariffApiManager(ICardTariffApiService cardTariffApiService)
		{
			_cardTariffApiService = cardTariffApiService;
		}

		public async Task<TariffApiModel> GetCurrentTariffAsync(long clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _cardTariffApiService.GetCurrentTariffAsync(clientId, cardId, cancellationToken);
		}
		public async Task<SearchRowsResponseRowModel<TariffApiModel>> GetAllTariffAsync(long clientId, long cardId, CancellationToken cancellationToken)
		{
			return await _cardTariffApiService.GetAllTariffAsync(clientId, cardId, cancellationToken);
		}

		public async Task<byte[]> GetTariffFileAsync(long clientId, long cardId, CancellationToken cancellationToken)
		{
			var pathSource = $"{AppContext.BaseDirectory}Files/tarif-BVR.pdf";
			return await FileHelper.GetFileAsync(pathSource);
		}
	}
}
