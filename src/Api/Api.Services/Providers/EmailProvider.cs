using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.Exceptions;
using Vostok.Hotline.Api.Services.Models;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Providers
{
	internal class EmailProvider : IMessengerProvider
	{
		private readonly EmailSettings _emailSettings;
		public EmailProvider(IServiceProvider serviceProvider)
		{
			_emailSettings = serviceProvider.GetRequiredService<EmailSettings>();
		}

		public ProviderTypeEnum ProviderType => ProviderTypeEnum.Email;

		public Task<SendingStatusApiModel> GetStatusAsync(MessageStatusRequestApiModel msg, CancellationToken cancellationToken)
		{
			throw new NotSupportedException();
		}

		public async Task<SendingStatusApiModel> SendAsync<EmailMessagePackageApiModel>(IMessagePackageApiModel<EmailMessagePackageApiModel> msg, CancellationToken cancellationToken)
		{
			List<MimeMessage> mailMessages = msg.GetMessage() as List<MimeMessage>;

			foreach (MimeMessage message in mailMessages)
			{
				try
				{
					if (!string.IsNullOrEmpty(_emailSettings.From))
					{
						message.From.Add(new MailboxAddress(_emailSettings.From, _emailSettings.From));
					}
					else
					{
#if DEBUG
						message.From.Add(new MailboxAddress("noreply@bank.bank", "noreply@bank.bank"));
#else
						throw new MessengerApiException("Email 'From' address not set");
#endif
					}

					using (SmtpClient client = new SmtpClient())
					{
						client.CheckCertificateRevocation = _emailSettings.CheckCertRevocation;
						client.Connect(_emailSettings.SmtpHost, _emailSettings.SmtpPort, SecureSocketOptions.StartTlsWhenAvailable);
						if (!string.IsNullOrEmpty(_emailSettings.SmtpLogin))
						{
							client.Authenticate(_emailSettings.SmtpLogin, _emailSettings.SmtpPassword);
						}
						await client.SendAsync(message, cancellationToken);
						client.Disconnect(true);
					}
				}
				catch (Exception e)
				{
					throw new MessengerApiException(e.Message, e);
				}
			}

			return new SendingStatusApiModel()
			{
				Status = new MessageStatusApiModel()
				{
					Status = MessageStatusEnum.SENT,
					StatusTime = DateTime.Now
				}
			};
		}
	}
}
