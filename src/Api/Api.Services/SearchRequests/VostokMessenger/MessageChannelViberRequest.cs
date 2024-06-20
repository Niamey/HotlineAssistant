using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	public class MessageChannelViberRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("ttl")]
		public long Ttl { get; set; }

		[JsonProperty("imageUrl")]
		public string ImageUrl { get; set; }

		[JsonProperty("caption")]
		public string Caption { get; set; }

		[JsonProperty("actionUrl")]
		public string ActionUrl { get; set; }

		[JsonProperty("iosExpirityText")]
		public string IosExpirityText { get; set; }
	}
}
