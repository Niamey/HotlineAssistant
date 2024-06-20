using System;
using System.Diagnostics.CodeAnalysis;

namespace Vostok.HotLineAssistant.HttpAggregator.Services.Contact
{
	[SuppressMessage("ReSharper", "NotAccessedField.Local", Justification = "For demonstration purposes.")]
	public class ContactServiceUrlProvider : IContactServiceUrlProvider
	{
		private readonly Uri _baseUrl;
		private readonly Uri _clientServiceBaseUrl;

		public ContactServiceUrlProvider(Uri baseUrl, Uri clientServiceBaseUrl)
		{
			_baseUrl = baseUrl ?? throw new ArgumentNullException(nameof(baseUrl));
			_clientServiceBaseUrl = clientServiceBaseUrl ?? throw new ArgumentNullException(nameof(clientServiceBaseUrl));
		}
	}
}