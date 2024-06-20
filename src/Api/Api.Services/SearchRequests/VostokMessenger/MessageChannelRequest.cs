using System;
using Newtonsoft.Json;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MessageChannelRequest
	{
		[JsonProperty("viber")]
		public MessageChannelViberRequest Viber { get; set; }

		[JsonProperty("sms")]
		public MessageChannelSmsRequest Sms { get; set; }

		public void Add(IMessageApiModel message)
		{
			switch (message.MessageType)
			{
				case MessageTypeEnum.SMS:
					Sms = (MessageChannelSmsRequest)message.GetMessage();
					break;

				case MessageTypeEnum.Viber:
					Viber = (MessageChannelViberRequest)message.GetMessage();
					break;

				default:
					throw new NotImplementedException();
			}
		}
	}
}
