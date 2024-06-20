using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public class ClientService : IClientService
	{
		private readonly IHttpClientFactory _clientFactory;
		public ClientService(IHttpClientFactory clientFactory)
		{
			_clientFactory = clientFactory;
		}

		public async Task<CounterPartResponseModel> ClientSearch(ClientSearchCriteria searchCriteria)
		{
			HttpResponseMessage response = null;
			var client = _clientFactory.CreateClient("crm");
			var url =
				$"Counterpart?Code={searchCriteria.Code}&Criteria={searchCriteria.Criteria}&Type={searchCriteria.Type}";

			if (searchCriteria.CurrentPage.HasValue && searchCriteria.PageSize.HasValue)
				url =
					$"{url}&Paging.CurrentPage{searchCriteria.CurrentPage.Value}&Paging.PageSize={searchCriteria.PageSize.Value}";

			if (!string.IsNullOrEmpty(searchCriteria.SortColumn))
				url = $"{url}&Sorting.Column={searchCriteria.SortColumn}";

			if (searchCriteria.IsDescending.HasValue)
				url = $"{url}&Sorting.IsDescending={searchCriteria.IsDescending.Value}";
			using (var request = new HttpRequestMessage(HttpMethod.Get, url))
			{
				request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				response = await client.SendAsync(request).ConfigureAwait(false);
			}
			var json = await response.Content.ReadAsStringAsync();
			return JsonConvert.DeserializeObject<CounterPartResponseModel>(json);
		}
	}
}