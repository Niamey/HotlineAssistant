using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Transactions;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.Responses.Transactions;
using Vostok.Hotline.Api.Retail.SearchRequests.Transactions;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices.Transactions
{
	internal class TransactionApiService : ITransactionApiService
	{
		private readonly RetailSystemInformationProviderClient _httpClient;
		private readonly EnvironmentManager _environmentManager;


		internal TransactionApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}
		public async Task<TransactionCollectionApiModel> GetTransactionCollectionAsync(long clientId, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize, CancellationToken cancellationToken) {
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.TransactionsUrl, cancellationToken);
			var query = new TransactionCollectionSearchRequest(clientId, dateFrom, dateTo, pageIndex, pageSize).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<TransactionCollectionResponseModel>(jsonResult);

			return result.ToTransactionCollectionApiModel();
		}
	}
}