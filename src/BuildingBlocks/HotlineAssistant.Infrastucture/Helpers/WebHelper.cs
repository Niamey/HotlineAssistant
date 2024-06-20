using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.Infrastucture.Helpers
{

	public class WebHelper : IWebHelper
    {
        private readonly HttpClient _client;

        private WebHelper(){ }

        public WebHelper(HttpClient client) : this()
        {
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "HotLineAssistant");
            _client = client;
        }
        
        public async Task<string> GetAsync(string url)
        {
            using var response = await _client.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostAsync(string url, string jsonData)
        {
            using HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
            using var response = await _client.PostAsync($"{_client.BaseAddress}{url}", content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}