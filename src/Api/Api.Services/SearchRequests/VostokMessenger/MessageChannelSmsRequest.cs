using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	public class MessageChannelSmsRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("ttl")]
		public long Ttl { get; set; }

		[JsonProperty("isTranslit")]
		public bool IsTranslit { get; set; }
	}
}
