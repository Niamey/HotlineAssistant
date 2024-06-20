using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.ApiServices.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Models.Transactions;
using Vostok.Hotline.Api.Retail.ProviderClients;
using Vostok.Hotline.Api.Retail.Requests.Commands.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards.Enums;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Core.Common.Configurations;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Common.Security;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class CardApiService : ICardApiService
	{
		private readonly RetailSystemInformationProviderClient _httpInformationClient;
		private readonly RetailSystemManagementProviderClient _httpManagementClient;
		private readonly EnvironmentManager _environmentManager;

		private readonly HotlineEnvironment _hotlineEnvironment;
		private readonly UserSession _userSession;

		internal CardApiService(IServiceProvider serviceProvider)
		{
			_httpInformationClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_httpManagementClient = serviceProvider.GetRequiredService<RetailSystemManagementProviderClient>();
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();

			_hotlineEnvironment = serviceProvider.GetRequiredService<HotlineEnvironment>();
			_userSession = serviceProvider.GetRequiredService<UserSession>();
		}

		public async Task<CardBalanceApiModel> GetCardBalanceAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.BalancesUrl, cancellationToken);
			var query = new CardBalanceSearchRequest(clientId, cardId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpInformationClient.GetAsync(uri, cancellationToken);
			var result = JsonHelper.FromJson<List<CardBalanceResponseModel>>(jsonResult);

			return result?.FirstOrDefault()?.MapToCardBalanceApiModel();
		}

		public async Task<CardCollectionApiModel> GetCardCollectionAsync(long clientId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardsUrl, cancellationToken);
			var query = new CardCollectionSearchRequest(clientId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpInformationClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CardCollectionResponseModel>(jsonResult);

			return result.ToCardCollectionApiModel();
		}

		public async Task<CardApiModel> GetCardAsync(long? clientId, long cardId, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardsUrl, cancellationToken);
			var query = new CardSearchRequest(clientId, cardId).GetUrlQuery();
			var uri = new Uri($"{host}?{query}");

			var jsonResult = await _httpInformationClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CardCollectionResponseModel>(jsonResult)?.FirstOrDefault();

			return result?.ToCardApiModel();
		}

		public async Task<CardChangeStatusResultApiModel> ChangeStatusAsync(long cardId, CardStatusRetailEnum status, string comment, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardsChangeStatusUrl, cancellationToken);
			var uri = new Uri($"{host}");

			var commentObject = new CardChangeStatusCommentCommand
			{
				Comment = comment,
				SystemName = _hotlineEnvironment.ProjectName,
				UserLogin = _userSession.CurrentUser.Login,
				IsEmployee = true,
				ModifyDate = DateTime.Now
			};

			var request = new CardChangeStatusCommand(cardId, status, commentObject);
			var json = JsonHelper.ToJson(request);

			var response = await _httpManagementClient.PutTryAsync(uri, json, cancellationToken);
			if (!response.IsSuccessStatusCode)
				throw new FailedRequestException(response);

			var result = new StatusCommandApiViewModel()
			{
				Success = true,
				Message = status == CardStatusRetailEnum.Active ? "Карта активна" : "Статус картки змінений"
			};

			return new CardChangeStatusResultApiModel()
			{
				Status = result,
				Comment = commentObject.ToChangeStatusComment()
			};
		}
	}
}