using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using MimeKit;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class EmailMessageApiModel : IMessageApiModel<MimeMessage>
	{
		public MessageTypeEnum MessageType => MessageTypeEnum.Email;

		[Required]
		[StringLength(150, MinimumLength = 5)]
		public string Message { get; set; }
		[Required]
		public string Subject { get; set; }
		[Required]
		public string To { get; set; }
		public string Copy { get; set; }
		public bool IsBodyHtml { get; set; }
		public MessageFileCollectionApiModel<MessageFileApiModel> Attachments { get; set; }

		public MimeMessage GetMessage()
		{
			MimeMessage emailMessage = new MimeMessage();
			BodyBuilder bodyBuilder = new BodyBuilder();
			InternetAddressList list;

			if (!string.IsNullOrEmpty(To))
			{
				list = new InternetAddressList();
				foreach (string address in To.Split(';').Where(s => !string.IsNullOrEmpty(s)).Distinct())
				{
					list.Add(new MailboxAddress(address, address));
				}
				emailMessage.To.AddRange(list);
			}
			if (!string.IsNullOrEmpty(Copy))
			{
				list = new InternetAddressList();
				foreach (string address in Copy.Split(';').Where(s => !string.IsNullOrEmpty(s)).Distinct())
				{
					list.Add(new MailboxAddress(address, address));
				}
				emailMessage.Cc.AddRange(list);
			}

			emailMessage.Subject = Subject;

			if (IsBodyHtml)
				bodyBuilder.HtmlBody = Message;
			else
				bodyBuilder.TextBody = Message;

			if (Attachments != null)
			{
				foreach (MessageFileApiModel file in Attachments)
				{
					using (Stream stream = new MemoryStream(file.Data))
					{
						bodyBuilder.Attachments.Add(file.FileName, stream);
					}
				}
			}

			emailMessage.Body = bodyBuilder.ToMessageBody();
			return emailMessage;
		}
	}
}
