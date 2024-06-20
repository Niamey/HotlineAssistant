using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Retail.Abstractions;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Managers;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.ConstantNames;
using Vostok.Hotline.Core.Common.Enums.Messenger;
using Vostok.Hotline.Core.Common.Exeptions;
using Vostok.Hotline.Data.Repository.Core.Managers;

namespace Vostok.Hotline.Api.Retail.ApiServices.Cards
{
	internal class CardBlockingApiService : ICardBlockingApiService
	{
		private readonly EnvironmentManager _environmentManager;
		private readonly MessengerApiManager _messengerApiManager;

		internal CardBlockingApiService(IServiceProvider serviceProvider)
		{
			_environmentManager = serviceProvider.GetRequiredService<EnvironmentManager>();
			_messengerApiManager = serviceProvider.GetRequiredService<MessengerApiManager>();
		}
		
		public async Task<StatusCommandApiViewModel> SendEmailAsync(string subject, string body, CancellationToken cancellationToken)
		{
			string email = await _environmentManager.GetSettingValueAsync(EnvironmentConstant.ApiEndpoint.RetailApi.CardBlockingEmail, cancellationToken);

			if (string.IsNullOrEmpty(email))
				throw new FailedRequestException($"Email is empty");
		 
			//Send email
			var messagePackage = new EmailMessagePackageApiModel();
			var package = new EmailMessageApiModel()
			{
				To = email,
				IsBodyHtml = true,
				Message = body,
				Subject = subject
			};
			messagePackage.Package = new MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>>()
			{
				package
			};

			var sendEmailResult = await _messengerApiManager.SendMessageAsync(messagePackage, cancellationToken);
			if (sendEmailResult.Status.Status != MessageStatusEnum.SENT)
				throw new FailedRequestException("Error sending mail");

			return new StatusCommandApiViewModel()
			{
				Success = true,
				Message = "Повідомлення успішно відправлено"
			};
		}
	}
}
