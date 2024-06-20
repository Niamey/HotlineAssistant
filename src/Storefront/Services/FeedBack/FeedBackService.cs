using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums.Messenger;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Documents.Requests;
using Vostok.Hotline.Core.Documents.Services;
using Vostok.Hotline.Data.Repository.Core.Managers;
using Vostok.Hotline.Document.Templates.FeedBack;
using Vostok.Hotline.Storefront.Requests.Commands;

namespace Vostok.Hotline.Storefront.Services.FeedBack
{
	public class FeedBackService
	{
		private readonly DocumentService _documentService;
		private readonly MessengerApiManager _messengerApiManager;
		private readonly EnvironmentManager _environmentManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly ISessionContext _sessionContext;

		public FeedBackService(DocumentService documentService, MessengerApiManager messengerApiManager, EnvironmentManager environmentManager, IHttpContextAccessor httpContextAccessor, ISessionContext sessionContext)
		{
			_documentService = documentService;
			_messengerApiManager = messengerApiManager;
			_environmentManager = environmentManager;
			_httpContextAccessor = httpContextAccessor;
			_sessionContext = sessionContext;
		}

		public async Task<StatusCommandViewModel> SendAsync(SendFeedBackCommand request, CancellationToken cancellationToken)
		{
			//Create Email Template
			var resultTemplate = await _documentService.RenderAsync(new RenderQuery<ITemplateViewModel>()
			{
				TemplatePath = "~/FeedBack/FeedBackTemplate.cshtml",
				TokenModel = new FeedBackModel()
				{
					FullName = request.FullName,
					Login = request.Login,
					Email = request.Email,
					Position = request.Position,
					Message = request.Message,
					Rating = request.Rating.ToString()
				}
			}, cancellationToken);

			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.FeedBackEmail, cancellationToken),
				IsBodyHtml = true,
				Message = resultTemplate.Result,
				Subject = "FeedBack Hotline Assistant"
			};

			messagePackage.Package = new MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>>()
			{
				package
			};

			var sendEmailResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);

			if (sendEmailResult.Status.Status != MessageStatusEnum.SENT)
				return new StatusCommandViewModel()
				{
					Success = false,
					Message = "Помилка при відправці повідомлення"
				};

			return new StatusCommandViewModel()
			{
				Success = true,
				Message = "Повідомлення успішно відправлено"
			};
		}

		public async Task<StatusCommandViewModel> ReportExceptionsAsync(ReportExceptionsCommand request, CancellationToken cancellationToken)
		{
			//Create Email Template
			var resultTemplate = await _documentService.RenderAsync(new RenderQuery<ITemplateViewModel>()
			{
				TemplatePath = "~/FeedBack/ReportExceptionsTemplate.cshtml",
				TokenModel = new ReportExceptionsModel()
				{
					Exceptions = request.Exceptions,
					Request = _httpContextAccessor.HttpContext.Request,
					SessionId = _sessionContext.SessionId,
					RequestedUrl = request.RequestedUrl
				}
			}, cancellationToken);

			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.StorefrontApi.ReportExceptionsEmail, cancellationToken),
				IsBodyHtml = true,
				Message = resultTemplate.Result,
				Subject = "Report Hotline Assistant"
			};

			messagePackage.Package = new MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>>()
			{
				package
			};

			var sendEmailResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);

			if (sendEmailResult.Status.Status != MessageStatusEnum.SENT)
				return new StatusCommandViewModel()
				{
					Success = false,
					Message = "Помилка при відправці повідомлення"
				};

			return new StatusCommandViewModel()
			{
				Success = true,
				Message = "Повідомлення успішно відправлено"
			};
		}
	}
}
