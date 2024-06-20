using System;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class MessagePackageStatusApiModel
	{
		public MessageTypeEnum MessageType { get; set; }
		public DateTime StatusTime { get; set; }
		public MessageStatusEnum Status { get; set; }
	}
}
