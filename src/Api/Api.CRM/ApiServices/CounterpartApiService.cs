using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Responses.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Api.CRM.Mapper;
using Vostok.Hotline.Api.CRM.Abstractions;
using Vostok.Hotline.Api.CRM.Responses.SearchRequests;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Core.Common.ConstantNames;

namespace Vostok.Hotline.Api.CRM.ApiServices
{
	internal class CounterpartApiService : ICounterpartApiService
	{
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal CounterpartApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<SearchPagedResponseRowModel<CounterpartApiModel>> SearchAsync(CounterpartRequest request, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.Crm.CounterpartUrl, cancellationToken);
			var query = request.GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var counterparts = JsonHelper.FromJson<CounterpartResponseRowModel>(jsonResult);

			return counterparts.MapToCounterpartApiModel();
		}


		public async Task<CounterpartCountApiModel> GetTotalCountAsync(CounterpartCountRequest request, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.Crm.CounterpartCountUrl, cancellationToken);
			var query = request.GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CounterpartResponseRowCountModel>(jsonResult);

			return result.ToCounterpartCountApiModel();
		}
	}
}