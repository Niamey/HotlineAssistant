using System.Net.Http;
using Vostok.Hotline.Api.Base.Abstractions;
using Vostok.Hotline.Api.Retail.ProviderClients.Configurations;

namespace Vostok.Hotline.Api.Retail.ProviderClients
{
	internal class RetailSystemManagementProviderClient : KeyCloakHttpBaseClient
	{
		public RetailSystemManagementProviderClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
		{
		}

		protected override string KeyClockConfigName => nameof(RetailSystemManagementConfig);
	}
}