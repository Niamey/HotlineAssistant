using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Models.Agreements;
using Vostok.Hotline.Api.Retail.Responses.Agreements;
using Vostok.Hotline.Api.Retail.Mapper.Agreements;
using Vostok.Hotline.Api.Retail.SearchRequests.Agreements;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Core.Common.ConstantNames;
using System.Collections.Generic;
using Vostok.Hotline.Api.Retail.ProviderClients;

namespace Vostok.Hotline.Api.Retail.ApiServices.Agreements
{
	internal class AgreementApiService : IAgreementApiService
	{
		private readonly RetailSystemInformationProviderClient _httpClient;
		private readonly EnvironmentManager _environmentManager;


		internal AgreementApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}

		public async Task<AgreementCollectionApiModel> GetAgreementCollectionAsync(long clientId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.AgreementsUrl, cancellationToken);
			var query = new AgreementCollectionSearchRequest(clientId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			string jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<AgreementCollectionResponseModel>(jsonResult);
			return result.ToAgreementListViewModel();
		}

		public async Task<AgreementApiModel> GetAgreementAsync(long? clientId, long agreementId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.AgreementsUrl, cancellationToken);
			var query = new AgreementSearchRequest(clientId, agreementId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<AgreementCollectionResponseModel>(jsonResult)?.FirstOrDefault();

			return result?.ToAgreementViewModel();
		}

		public async Task<AgreementBalanceApiModel> GetBalanceAsync(long? clientId, long agreementId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.BalancesUrl, cancellationToken);
			var query = new AgreementBalanceSearchRequest(clientId, agreementId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);
			var result = JsonHelper.FromJson<List<AgreementBalanceResponseModel>>(jsonResult);

			return result?.FirstOrDefault()?.MapToAgreementBalanceApiModel();
		}

		public async Task<AgreementCreditParamsApiModel> GetCreditParamsAsync(long agreementId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.BalancesUrl, cancellationToken);
			var uri = new Uri($"{host}/{new AgreementCreditParamsSearchRequest(agreementId).GetUrlQuery()}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<AgreementCreditParamsResponseModel>(jsonResult);

			return result?.ToAgreementCreditParamsApiModel();
		}
	}
}