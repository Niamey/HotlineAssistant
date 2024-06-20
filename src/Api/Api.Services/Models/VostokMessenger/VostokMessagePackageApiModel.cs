using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Api.Services.SearchRequests;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class VostokMessagePackageApiModel : IMessagePackageApiModel<MessagePackageRequest>
	{
		// <summary>
		/// Номер телефону одержувача повідомлення (у форматі +380000000000)
		/// </summary>
		public string Recipient { get; set; }
		/// <summary>
		/// Зовнішній код повідомлення на стороні агента
		/// </summary>
		public string ExternalId { get; set; }
		/// <summary>
		/// Ознака, що повідомлення є рекламним
		/// </summary>
		public bool IsPromotional { get; set; }

		public ProviderTypeEnum ProviderType => ProviderTypeEnum.VostokMessenger;

		public MessagePackageCollectionApiModel<IMessageApiModel> Package { get; set; }

		public MessagePackageRequest GetMessage()
		{
			MessagePackageRequest package = new MessagePackageRequest();

			package.ExternalId = ExternalId;
			package.Recipient = Recipient;
			package.IsPromotional = IsPromotional;
			package.Channel = new MessageChannelRequest();

			foreach (var item in Package) 
			{
				package.Channel.Add(item);
			}

			return package;
		}
	}
}
