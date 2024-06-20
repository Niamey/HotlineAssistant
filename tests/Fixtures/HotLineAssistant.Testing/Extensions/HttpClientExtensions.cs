using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HotLineAssistant.Testing.Extensions
{
	public static class HttpClientExtensions
	{
		private const string MediaType = "application/json";

		public static Task<HttpResponseMessage> PostCommand(this HttpClient client, string url, object command)
		{
			return client.PostAsync(url, MapToContent(command));
		}

		public static Task<HttpResponseMessage> PutCommand(this HttpClient client, string url, object command)
		{
			return client.PutAsync(url, MapToContent(command));
		}

		private static StringContent MapToContent<T>(T command) where T : class
		{
			return new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, MediaType);
		}
	}
}