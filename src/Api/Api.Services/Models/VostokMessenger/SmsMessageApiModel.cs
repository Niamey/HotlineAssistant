using System.ComponentModel.DataAnnotations;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class SmsMessageApiModel: IMessageApiModel<MessageChannelSmsRequest>
	{
		public SmsMessageApiModel()
		{ 
		}
		public SmsMessageApiModel(string message, int? ttl)
		{
			Message = message;
			Ttl = ttl;
		}
		public MessageTypeEnum MessageType => MessageTypeEnum.SMS;
		[Required]
		[StringLength(150, MinimumLength = 5)]
		public string Message { get; set; }
		public int? Ttl { get; set; }
		public bool IsTranslit { get; set; }
		public MessageChannelSmsRequest GetMessage()
		{
			return new MessageChannelSmsRequest()
			{
				Text = Message,
				IsTranslit = IsTranslit,
				Ttl = (long)Ttl
			};
		}

	}
}
