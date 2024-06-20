using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.HttpAggregator.Services
{
	public enum HttpRequestOptions
	{
		None,
		ThrowOnNotFound
	}

	public abstract class HttpService
	{
		private readonly HttpClient _httpClient;

		protected HttpService(HttpClient httpClient)
		{
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		}

		protected async Task<string> SendRequest(HttpRequestMessage request, HttpRequestOptions options = HttpRequestOptions.None)
		{
			var response = await _httpClient.SendAsync(request);

			switch (response.StatusCode)
			{
				case HttpStatusCode.NotFound when options != HttpRequestOptions.ThrowOnNotFound:
					return null;
				case HttpStatusCode.BadRequest:
					ThrowValidationException(
						await response.Content.ReadAsStringAsync());
					break;
			}

			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		private static void ThrowValidationException(string content)
		{
			throw new ValidationException
			{
				Errors = string.IsNullOrEmpty(content) ? null : JsonConvert.DeserializeObject(content)
			};
		}

		protected StringContent MapToContent<T>(T data) where T : class
		{
			return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
		}
	}
}