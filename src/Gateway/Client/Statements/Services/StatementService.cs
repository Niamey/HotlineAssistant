using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Queries;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Commands;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Api.CRM.Models;
using Vostok.Hotline.Api.CRM.Managers;
using Vostok.Hotline.Api.CRM.Extensions;
using Vostok.Hotline.Document.Templates.Statements;
using Vostok.Hotline.Core.Documents.Models.Responses;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Documents.Requests;
using Vostok.Hotline.Core.Documents.Services;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Gateway.Client.Statements.Services
{
	public class StatementService
	{
		private readonly StatementApiManager _statementApiManager;
		private readonly CounterpartApiManager _counterpartApiManager;
		private readonly DocumentService _documentService;
		private readonly MessengerApiManager _messengerApiManager;

		public StatementService(StatementApiManager statementApiManager, CounterpartApiManager counterpartApiManager, 
			DocumentService documentService, MessengerApiManager messengerApiManager)
		{
			_statementApiManager = statementApiManager;
			_counterpartApiManager = counterpartApiManager;
			_documentService = documentService;
			_messengerApiManager = messengerApiManager;
		}

		public async Task<StatementViewModel> GetStatementAsync(StatementQuery request, CancellationToken cancellationToken)
		{
			var result = await _statementApiManager.GetStatementAsync(request.AgreementId.Value, request.DateStart.Value, request.DateEnd.Value, cancellationToken);
						
			if (result != null)
				return new StatementViewModel { Data = result }; 
			else
				throw new NotFoundRequestException();
		}

		private async Task<CounterpartApiModel> GetBySolarIdAsync(long solarId, CancellationToken cancellationToken)
		{
			var counterpart = await _counterpartApiManager.GetBySolarIdAsync(solarId, cancellationToken);
			if (counterpart == null)
				throw new NotFoundRequestException($"Client with id: {solarId} not found");
			return counterpart;
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

		public async Task<StatusCommandViewModel> SendEmailAsync(StatementSendEmailCommand request, CancellationToken cancellationToken)
		{
			var counterpart = await GetBySolarIdAsync(request.SolarId.Value, cancellationToken);
			var counterpartEmail = counterpart.GetEmail();

#if DEBUG
			counterpartEmail = "s.grushko@bankvostok.com.ua";
#endif
			if (string.IsNullOrEmpty(counterpartEmail))
				throw new FailedRequestException($"Client with id: {request.SolarId} email empty");

			//Create Email Template
			var resultTemplate = await GetTemplateAsync("~/Statements/ClientStatementTemplate.cshtml", new ClientStatementModel() { ClientName = counterpart.FullName }, cancellationToken);

			//Get TariffFile
			var fileValue = await _statementApiManager.GetStatementAsync(request.AgreementId.Value, request.DateStart.Value, request.DateEnd.Value, cancellationToken);
			if (fileValue.Length == 0)
				throw new NotFoundRequestException("Statement file empty");

			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = counterpartEmail,
				IsBodyHtml = true,
				Message = resultTemplate.Result,
				Subject = "Картка Банку Власний Рахунок. Виписка",
				Attachments = new MessageFileCollectionApiModel<MessageFileApiModel>()
				{
					new MessageFileApiModel()
					{
						FileName = "Statement.html",
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

	}
}