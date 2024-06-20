using System.Net.Http;
using Vostok.Hotline.Api.Base.Abstractions;
using Vostok.Hotline.Api.Services.ProviderClients.Configurations;

namespace Vostok.Hotline.Api.Services.ProviderClients
{
	internal class RetailSystemCardShirtProviderClient : KeyCloakHttpBaseClient
	{
		public RetailSystemCardShirtProviderClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
		{
		}

		protected override string KeyClockConfigName => nameof(RetailSystemCardShirtConfig);
	}
}