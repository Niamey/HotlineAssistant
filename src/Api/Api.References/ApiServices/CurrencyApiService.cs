using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Mapper;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses;
using Vostok.Hotline.Api.References.SearchRequests;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.References.ApiServices
{
	internal class CurrencyApiService : ICurrencyApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;


		internal CurrencyApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<CurrencyApiModel> GetCurrencyAsync(string currencyCode, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ReferenceApi.GetCurrencyUrl, cancellationToken);
			var query = new CurrencySearchRequest(currencyCode).GetUrlQuery();

			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CurrencyResponseModel>(jsonResult);

			return result?.ToCurrencyApiModel();
		}
	}
}
