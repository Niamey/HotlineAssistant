using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Classifiers;
using Vostok.Hotline.Api.Retail.Models.Classifiers;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.Requests.Commands.Classifiers;
using Vostok.Hotline.Api.Retail.Responses.Classifiers;
using Vostok.Hotline.Api.Retail.Responses.Classifiers.Enums;
using Vostok.Hotline.Api.Retail.SearchRequests.Classifiers;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices
{
	internal class ClassifierApiService : IClassifierApiService
	{
		private readonly RetailSystemInformationProviderClient _httpClient;
		private readonly RetailSystemManagementProviderClient _httpManagementClient;
		private readonly EnvironmentManager _environmentManager;

		internal ClassifierApiService(IServiceProvider serviceProvider)
		{
			_httpClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_httpManagementClient = serviceProvider.GetRequiredService<RetailSystemManagementProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
		}
		public async Task<ClassifierCollectionApiModel> GetClassifiersAsync(ClassifierRetailTypeEnum type, long entityId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.ClassifiersUrl, cancellationToken);

			var query = new ClassifierSearchRequest(type, entityId).GetUrlQuery();
			var uri = new Uri($"{host}/{query}");

			var jsonResult = await _httpClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<ClassifierCollectionResponseModel>(jsonResult);

			return result?.ToClassifierCollectionApiModel();
		}

		public async Task<StatusCommandApiViewModel> SetClassifierAsync(ClassifierRetailTypeEnum type, long entityId, string classifierCode, string classifierValue, string comment, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.SetClassifierUrl, cancellationToken);
			var uri = new Uri($"{host}");

			var request = new SetClassifierCommand(type, entityId, classifierCode, classifierValue, comment);
			var json = JsonHelper.ToJson(request);

			var response = await _httpManagementClient.PutTryAsync(uri, json, cancellationToken);
			if (!response.IsSuccessStatusCode)
				throw new FailedRequestException(response);

			return new StatusCommandApiViewModel()
			{
				Success = true,
				Message = $"Класифікатор (типу {type}) {classifierCode} змінений на {classifierValue}"
			};
		}
	}
}
