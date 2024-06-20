using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Vostok.Hotline.Api.Services.Models;

namespace Vostok.Hotline.Api.Services.Responses.Messenger
{
	class MessageResponseStatusesModel
	{
		[JsonProperty("viber")]
		public MessageResponseStatusModel Viber { get; set; }

		[JsonProperty("sms")]
		public MessageResponseStatusModel Sms { get; set; }
	}
}
