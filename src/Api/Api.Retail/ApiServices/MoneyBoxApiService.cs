using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.MoneyBoxes;
using Vostok.Hotline.Api.Retail.Models;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.Responses.Accounts;
using Vostok.Hotline.Api.Retail.SearchRequests;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices
{
	internal class MoneyBoxApiService : IMoneyBoxApiService
	{
		private readonly RetailSystemInformationProviderClient _httpClient;
		private readonly EnvironmentManager _environmentManager;

		internal MoneyBoxApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<MoneyBoxCollectionApiModel> GetMoneyBoxCollectionAsync(long agreementId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.MoneyBoxesUrl, cancellationToken);
			var query = new MoneyBoxSearchRequest(agreementId).GetUrlQuery();

			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<MoneyBoxCollectionResponseModel>(jsonResult);

			return result?.ToMoneyBoxCollectionApiModel();
		}
	}
}
