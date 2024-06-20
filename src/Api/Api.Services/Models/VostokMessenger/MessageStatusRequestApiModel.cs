using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class MessageStatusRequestApiModel
	{
		/// <summary>
		/// Унікальний код повідомлення (messageId);
		/// </summary>
		public string MessagePackageId { get; set; }
		/// <summary>
		/// Зовнішній код повідомлення на стороні агента;
		/// </summary>
		public string ExternalId { get; set; }
		/// <summary>
		/// Унікальний код повідомлення Messanger.
		/// </summary>
		public long SmsGateId { get; set; }

		public ProviderTypeEnum ProviderType => ProviderTypeEnum.VostokMessenger;
	}
}
