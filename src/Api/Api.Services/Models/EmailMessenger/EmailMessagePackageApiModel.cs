using System.Collections.Generic;
using MimeKit;
using Vostok.Hotline.Api.Services.Abstractions;
using Vostok.Hotline.Core.Common.Enums.Messenger;

namespace Vostok.Hotline.Api.Services.Models
{
	public class EmailMessagePackageApiModel : IMessagePackageApiModel<List<MimeMessage>>
	{
		public ProviderTypeEnum ProviderType => ProviderTypeEnum.Email;

		public MessagePackageCollectionApiModel<IMessageApiModel<EmailMessageApiModel>> Package { get; set; }

		public List<MimeMessage> GetMessage()
		{
			List<MimeMessage> listMailMessage = new List<MimeMessage>();

			foreach (EmailMessageApiModel item in Package) {
				listMailMessage.Add(item.GetMessage());
			}
			return listMailMessage;
		}
	}
}
