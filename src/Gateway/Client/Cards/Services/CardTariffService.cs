using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.Retail.Managers;
using Vostok.Hotline.Api.Retail.SearchRequests.Cards;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Core.Documents.Requests;
using Vostok.Hotline.Core.Documents.Services;
using Vostok.Hotline.Gateway.Client.Cards.Mapper;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;
using Vostok.Hotline.Api.CRM.Extensions;
using Vostok.Hotline.Document.Templates.Cards.Tariffs;
using System;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;
using Vostok.Hotline.Core.Documents.Models.Responses;

namespace Vostok.Hotline.Gateway.Client.Cards.Services
{
	public class CardTariffService
	{
		private readonly CardTariffApiManager _cardTariffManager;
		private readonly CounterpartApiManager _counterpartApiManager;
		private readonly DocumentService _documentService;
		private readonly MessengerApiManager _messengerApiManager;
		private readonly SharedLinkApiManager _sharedLinkApiManager;
		private readonly EnvironmentManager _environmentManager;


		public CardTariffService(CardTariffApiManager cardTariffManager, CounterpartApiManager counterpartApiManager, DocumentService documentService, MessengerApiManager messengerApiManager,
			SharedLinkApiManager sharedLinkApiManager, EnvironmentManager environmentManager)
		{
			_cardTariffManager = cardTariffManager;
			_counterpartApiManager = counterpartApiManager;
			_documentService = documentService;
			_messengerApiManager = messengerApiManager;
			_sharedLinkApiManager = sharedLinkApiManager;
			_environmentManager = environmentManager;
		}
		public async Task<SearchRowsResponseRowModel<CardTariffViewModel>> GetAllTariffAsync(CardTariffCollectionSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await _cardTariffManager.GetAllTariffAsync(request.ClientId.Value, request.CardId.Value, cancellationToken);

			if (result != null)
				return result.ToCardTariffViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<CardTariffViewModel> GetCurrentTariffAsync(CardTariffSearchRequest request, CancellationToken cancellationToken)
		{
			var result = await _cardTariffManager.GetCurrentTariffAsync(request.ClientId.Value, request.CardId.Value, cancellationToken);

			if (result != null)
				return result.ToCardTariffViewModel();
			else
				throw new NotFoundRequestException();
		}

		public async Task<StatusCommandViewModel> SendEmailAsync(CardTariffSendEmailCommand request, CancellationToken cancellationToken)
		{
			var counterpart = await GetBySolarIdAsync(request.ClientId.Value, cancellationToken);
			var counterpartEmail = counterpart.GetEmail();

#if DEBUG
			counterpartEmail = "l.breskin@bankvostok.com.ua";
#endif
			if (string.IsNullOrEmpty(counterpartEmail))
				throw new FailedRequestException($"Client with id: {request.ClientId} email empty");

			//Create Email Template
			var resultTemplate = await GetTemplateAsync("~/Cards/Tariffs/EmailTemplate.cshtml", new EmailModel() { ClientName = counterpart.FullName }, cancellationToken);

			//Get TariffFile
			var fileValue = await _cardTariffManager.GetTariffFileAsync(request.ClientId.Value, request.CardId.Value, cancellationToken);
			if (fileValue.Length == 0)
				throw new NotFoundRequestException("Tariff file empty");

			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = counterpartEmail,
				IsBodyHtml = true,
				Message = resultTemplate.Result,
				Subject = "Картка Банку Власний Рахунок. Тариф",
				Attachments = new MessageFileCollectionApiModel<MessageFileApiModel>()
				{
					new MessageFileApiModel()
					{
						FileName = "tariffs.pdf",
						Data = fileValue
					}
				}
			};
			messagePackage.Package = new MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>>()
			{
				package
			};

			var sendEmailResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);
			if (sendEmailResult.Status.Status != MessageStatusEnum.SENT)
				throw new FailedRequestException("Error sending mail");

			return new StatusCommandViewModel()
			{
				Success = true,
				Message = "Повідомлення успішно відправлено"
			};
		}

		private async Task<RenderResponseModel> GetTemplateAsync(string path, ITemplateViewModel model, CancellationToken cancellationToken)
		{
			var result = await _documentService.RenderAsync(new RenderQuery<ITemplateViewModel>()
			{
				TemplatePath = path,
				TokenModel = model
			}, cancellationToken);

			if (result.HasError)
				throw new FailedRequestException(result.ErrorMessage);
			return result;
		}

		public async Task<StatusCommandViewModel> SendViberAsync(CardTariffSendViberCommand request, CancellationToken cancellationToken)
		{
			var counterpart = await GetBySolarIdAsync(request.ClientId.Value, cancellationToken);
#if DEBUG
			var finPhone = "+380631234567";
#else
			if (counterpart.FinancialPhone == null)
				throw new FailedRequestException($"Client id {request.ClientId} has empty financial phone");
			var finPhone = $"+{counterpart.FinancialPhone}";
#endif
			//Get shared link
			// https://content.vostok.bank/documents/tarif.pdf
			var resultLink = await _sharedLinkApiManager.GetEncodedLinkAsync(await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint. CardTariff.DownloadUrl, cancellationToken), "Tariff-BVR.pdf", cancellationToken);
			if (string.IsNullOrEmpty(resultLink))
				throw new FailedRequestException("Error in generating file link");

			//Create Templates
			var viberTemplate = await GetTemplateAsync("~/Cards/Tariffs/ViberTemplate.cshtml", new ViberModel() { ClientName = counterpart.FullName, Link = resultLink }, cancellationToken);
			var smsTemplate = await GetTemplateAsync("~/Cards/Tariffs/SmsTemplate.cshtml", new SmsModel() { Link = resultLink }, cancellationToken);


			//Message Package
			var messagePackage = new VostokMessagePackageApiModel() {
				ExternalId = Guid.NewGuid().ToString(),
				Recipient = finPhone,
				Package = new MessagePackageCollectionApiModel<IMessageApiModel>()
				{
					new ViberMessageApiModel()
					{
						Message = viberTemplate.Result,
						Ttl = 0
					},
					new SmsMessageApiModel()
					{
						Message = smsTemplate.Result,
						Ttl = 0,
						IsTranslit = true
					}
				}
			};

			var sendMessageResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);
			if (sendMessageResult.Status.Status != MessageStatusEnum.CREATED)
				throw new FailedRequestException("Error sending viber message");

			return new StatusCommandViewModel()
			{
				Success = true,
				Message = "Повідомлення Viber успішно відправлено"
			};

		}

		protected async Task<CounterpartApiModel> GetBySolarIdAsync(long solarId, CancellationToken cancellationToken)
		{
			var counterpart = await _counterpartApiManager.GetBySolarIdAsync(solarId, cancellationToken);
			if (counterpart == null)
				throw new NotFoundRequestException($"Client with id: {solarId} not found");
			return counterpart;
		}
	}
}
