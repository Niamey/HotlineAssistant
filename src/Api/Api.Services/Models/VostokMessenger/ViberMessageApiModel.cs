using System.ComponentModel.DataAnnotations;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class ViberMessageApiModel : IMessageApiModel<MessageChannelViberRequest>
	{
		public ViberMessageApiModel()
		{
		}
		public ViberMessageApiModel(string message, int? ttl)
		{
			Message = message;
			Ttl = ttl;
		}
		public MessageTypeEnum MessageType => MessageTypeEnum.Viber;
		[Required]
		[StringLength(150, MinimumLength = 5)]
		public string Message { get; set; }
		public int? Ttl { get; set; }
		public string ImageUrl { get; set; }

		public string Caption { get; set; }

		public string ActionUrl { get; set; }

		public string IosExpirityText { get; set; }

		public MessageChannelViberRequest GetMessage()
		{
			return new MessageChannelViberRequest()
			{
				Text = Message,
				Ttl = (long)Ttl,
				ActionUrl = ActionUrl,
				Caption = Caption,
				ImageUrl = ImageUrl,
				IosExpirityText = IosExpirityText
			};
		}
	}
}
