using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.References.Abstractions;
using Vostok.Hotline.Api.References.Mapper;
using Vostok.Hotline.Api.References.Models;
using Vostok.Hotline.Api.References.Responses;
using Vostok.Hotline.Api.References.SearchRequests;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.References.ApiServices
{
	public class DivisionApiService : IDivisionApiService
	{
		private readonly DivisionMapper _divisionMapper;
		private readonly HotlineHttpClient _httpClient;
		private readonly EnvironmentManager _environmentManager;


		public DivisionApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<HotlineHttpClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_divisionMapper = serviceProvider.GetRequiredService<DivisionMapper>();
		}

		public async Task<DivisionApiModel> GetDivisionAsync(int divisionId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.ReferenceApi.GetDivisionUrl, cancellationToken);
			var query = new DivisionSearchRequest(divisionId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<DivisionResponseModel>(jsonResult);

			return _divisionMapper?.MapToDivisionApiModel(result);
		}
	}
}
