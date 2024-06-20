using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Configurations;
using System;
using Vostok.Hotline.Gateway.Client.References.ViewModels;
using System.Threading;
using Vostok.Hotline.Gateway.Client.Addresses.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Helpers;
using System.Threading.Tasks;
using Vostok.Hotline.Gateway.Client.References.Services.Models.Responses;
using Vostok.Hotline.Gateway.Client.References.Mapper;

namespace Vostok.Hotline.Gateway.Client.References.Services
{
	public class CurrencyService
	{
		private readonly HttpClientManager _httpClient;
		private readonly HotlineEnvironment _environment;

		public CurrencyService(HttpClientManager httpClient, HotlineEnvironment environment)
		{
			_httpClient = httpClient;
			_environment = environment;
		}

		public async Task<CurrencyViewModel> GetCurrencyDetailAsync(CurrencyDetailRequest request, CancellationToken cancellationToken)
		{
			var host = _environment.GetEnvironmentVariable("ApiEndpoint.ReferenceApi.GetCurrencyUrl");
			var query = request.GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CurrencyResponseModel>(jsonResult);

			return result.ToCurrencyViewModel();
		}

	}
}