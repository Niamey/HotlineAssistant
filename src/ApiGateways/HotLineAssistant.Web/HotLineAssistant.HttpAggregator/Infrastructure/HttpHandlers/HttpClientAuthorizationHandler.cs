using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.HttpAggregator.Infrastructure.HttpHandlers
{
	public class HttpClientAuthorizationHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public HttpClientAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];

			if (!string.IsNullOrEmpty(authorizationHeader))
				request.Headers.Add("Authorization", new string[] { authorizationHeader });

			var token = await GetToken();
			if (token != null)
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			return await base.SendAsync(request, cancellationToken);
		}

		async Task<string> GetToken()
		{
			return await _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
		}
    }
}