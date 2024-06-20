using System;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class MessageStatusApiModel
	{
		public DateTime StatusTime { get; set; }
		public MessageStatusEnum Status { get; set; }
	}
}
