using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Mapper.CardService;
using Vostok.Hotline.Api.Services.Models.CardService;
using Vostok.Hotline.Api.Services.ProviderClients;
using Vostok.Hotline.Api.Services.Responses.CardService;
using Vostok.Hotline.Api.Services.SearchRequests.CardService;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Services.ApiServices
{
	internal class CardImageApiService : ICardImageApiService
	{
		private readonly RetailSystemCardShirtProviderClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal CardImageApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemCardShirtProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<CardImageApiModel> GetCardShirtAsync(string cardCode, CancellationToken cancellationToken)
		{
			CardImageResponseModel result;
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ApiServices.CardImage.GetCardShirtUrl, cancellationToken);
			var query = new CardImageSearchRequest(cardCode).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

		
			var response= await _httpClient.GetTryAsync(uri, null, cancellationToken);
			if (response.IsSuccessStatusCode)
				result = JsonHelper.FromJson<CardImageResponseModel>(response.Response);
			else
				result = new CardImageResponseModel();
	
			return result.ToCardImageApiModel();
		}
	}
}
