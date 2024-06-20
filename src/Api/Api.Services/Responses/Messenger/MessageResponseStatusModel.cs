using System;
using Vostok.Hotline.Api.Services.Responses.Messenger.Enums;

namespace Vostok.Hotline.Api.Services.Responses.Messenger
{
	internal class MessageResponseStatusModel
	{
		public DateTime StatusTime { get; set; }
		public MessageStatusServiceEnum StatusId { get; set; }
	}
}
