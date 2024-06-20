using System;
using System.Net.Http;

namespace Vostok.HotLineAssistant.HttpAggregator.Services.Contact
{
	public class HttpContactService : HttpService, IContactService
	{
		private readonly IContactServiceUrlProvider _urlProvider;

		public HttpContactService(HttpClient httpClient, IContactServiceUrlProvider urlProvider)
			: base(httpClient)
		{
			_urlProvider = urlProvider ?? throw new ArgumentNullException(nameof(urlProvider));
		}
	}
}