using Newtonsoft.Json;

namespace Vostok.Hotline.Api.Services.SearchRequests
{
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public class MessagePackageRequest
	{
		/// <summary>
		/// Номер телефону одержувача повідомлення (у форматі +380000000000)
		/// </summary>
		[JsonProperty("recipient")]
		public string Recipient { get; set; }
		/// <summary>
		/// Зовнішній код повідомлення на стороні агента
		/// </summary>
		[JsonProperty("externalId")]
		public string ExternalId { get; set; }
		/// <summary>
		/// Ознака, що повідомлення є рекламним
		/// </summary>
		[JsonProperty("isPromotional")]
		public bool IsPromotional { get; set; }
		/// <summary>
		/// Набор сообщений (для каждого типа свой провайдера (viber, sms etc))
		/// </summary>
		[JsonProperty("channel")]
		public MessageChannelRequest Channel { get; set; }
	}
}
