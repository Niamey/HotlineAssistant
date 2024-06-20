using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.Responses.Cards;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class CardHistoryApiService : ICardHistoryApiService
	{
		private readonly RetailSystemInformationProviderClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal CardHistoryApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<CardCollectionHistoryStatusApiModel> GetCardHistoryStatusAsync(long cardId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardsHistoryStatusUrl, cancellationToken);
			host = host.Replace("{cardId}", new CardHistoryStatusSearchRequest(cardId).GetUrlQuery());
			var uri = new Uri($"{host}");

			string jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CardCollectionHistoryStatusResponseModel>(jsonResult);

			return result?.ToCardCollectionHistoryStatusApiModel();
		}
	}
}
