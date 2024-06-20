using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Retail.Mapper.Cards;
using Vostok.Hotline.Api.Retail.Models.Cards;
using Vostok.Hotline.Api.Retail.Responses.Cards;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Api.Retail.Requests.Commands.Cards;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Managers;
using Vostok.Hotline.Core.Documents.Requests;
using Vostok.Hotline.Core.Documents.Services;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Document.Templates.Cards.Limits.Models;
using Vostok.Hotline.Api.Retail.ConstantNames;
using System.Linq;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;
using Vostok.Hotline.Api.Retail.ProviderClients;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class CardLimitApiService: ICardLimitApiService
	{
		private readonly EnvironmentManager _environmentManager;
		private readonly DocumentService _documentService;
		private readonly MessengerApiManager _messengerApiManager;
		private readonly CardApiManager _cardApiManager;
		private readonly AgreementApiManager _agreementApiManager;
		private readonly RetailSystemInformationProviderClient _httpInformationClient;
		private readonly RetailSystemManagementProviderClient _httpManagementClient;

		internal CardLimitApiService(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_documentService = serviceProvider.GetRequiredService<DocumentService>();
			_messengerApiManager = serviceProvider.GetRequiredService<MessengerApiManager>();
			_cardApiManager = serviceProvider.GetRequiredService<CardApiManager>();
			_agreementApiManager = serviceProvider.GetRequiredService<AgreementApiManager>();
			_httpInformationClient = serviceProvider.GetRequiredService<RetailSystemInformationProviderClient>();
			_httpManagementClient = serviceProvider.GetRequiredService<RetailSystemManagementProviderClient>();
		}

		public async Task<StatusCommandApiViewModel> ChangeCardLimitStatusAsync(long? clientId, long cardId, bool turnOn, CancellationToken cancellationToken)
		{
			CardSetLimitApiModel limitApiModel = null;

			var limitDetail = await GetLimitAsync(cardId, CardLimitConstant.CashCard, cancellationToken);

			if (limitDetail != null)
			{
				var limitId = limitDetail.Id;
				limitApiModel = await SetLimitAsync(cardId, limitId, turnOn, cancellationToken);
			}

			if (limitApiModel?.LimitId > 0)
			{
				return new StatusCommandApiViewModel
				{
					Success = true,
					Message = turnOn ? "Ліміт встановлений" : "Ліміт відключений"
				};
			}
			else
			{
				return new StatusCommandApiViewModel
				{
					Success = false,
					Message = "Зміна ліміту на зняття готівки недоступно для обраного продукту"
				};
			}
		}

		public async Task<StatusCommandApiViewModel> ChangeRiskAsync(long clientId, long cardId, decimal limit, bool isP2PLimited, DateTime? limitSetDate, string phone, CancellationToken cancellationToken)
		{
			string email = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardLimitEmail, cancellationToken);

			if (string.IsNullOrEmpty(email))
				throw new FailedRequestException($"Email is empty");

			var cardDetail = await _cardApiManager.GetCardAsync(clientId, cardId, cancellationToken);
			if (cardDetail == null)
				throw new NotFoundRequestException($"Card not found");

			var agreementDetail = await _agreementApiManager.GetAgreementAsync(clientId, cardDetail.AgreementId, cancellationToken);
			if (agreementDetail == null)
				throw new NotFoundRequestException($"Agreement not found");

			//Create Email Template
			var resultTemplate = await _documentService.RenderAsync(new RenderQuery<ITemplateViewModel>()
			{
				TemplatePath = $"~/Cards/Limits/Templates/ChangeRiskTemplate.cshtml",
				TokenModel = new ChangeRiskModel()
				{ 
					AgreementNumber = agreementDetail.Number,
					CardNumber = cardDetail.GetMaskedCardNumber(),
					Limit = limit,
					IsP2PLimited = isP2PLimited,
					LimitSetDate = limitSetDate,
					Phone = phone
				}
			}, cancellationToken);

			if (resultTemplate.HasError)
				throw new FailedRequestException(resultTemplate.ErrorMessage);

			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = email,
				IsBodyHtml = true,
				Message = resultTemplate.Result,
				Subject = $"Зміна ліміту на зняття готівки в ризиковіх країнах"
			};
			messagePackage.Package = new MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>>()
			{
				package
			};

			var sendEmailResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);
			if (sendEmailResult.Status.Status != MessageStatusEnum.SENT)
				throw new FailedRequestException("Error sending mail");

			return new StatusCommandApiViewModel
			{
				Success = true,
				Message = "Повідомлення успішно відправлено"
			};
		}

		public async Task<CardSetLimitApiModel> SetLimitAsync(long cardId, int limitId, bool enabled, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardSetLimitUrl, cancellationToken);
			var uri = new Uri($"{host}");

			var request = new CardSetLimitCommand(cardId, limitId, enabled);
			var json = JsonHelper.ToJson(request);

			var response = await _httpManagementClient.PutTryAsync(uri, json, cancellationToken);
			if (!response.IsSuccessStatusCode)
				throw new FailedRequestException(response);

			var result = JsonHelper.FromJson<CardSetLimitResponseModel>(response.Response);

			return result.ToCardCardSetLimitApiModel();

		}

		public async Task<CardCollectionLimitApiModel> GetLimitsAsync(long cardId, string limiterCode, CancellationToken cancellationToken)
		{
			var host = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardLimitsUrl, cancellationToken);
			host = host.Replace("{cardId}", cardId.ToString());

			var query = new CardLimitsSearchRequest(limiterCode).GetUrlQuery();

			var uri = new Uri($"{host}?{query}");

			string jsonResult = await _httpInformationClient.GetAsync(uri, cancellationToken);

			var result = JsonHelper.FromJson<CardCollectionLimitResponseModel>(jsonResult);
			return result.ToCardCollectionLimitApiModel();
		}

		public async Task<CardLimitApiModel> GetLimitAsync(long cardId, string limiterCode, CancellationToken cancellationToken)
		{
			var result = await GetLimitsAsync(cardId, limiterCode, cancellationToken);
			return result?.FirstOrDefault();
		}
	}
}