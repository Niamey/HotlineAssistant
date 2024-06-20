using Moq;
using Moq.Language.Flow;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HotLineAssistant.Testing.Extensions.Moq
{
	public static class HttpClientExtensions
	{
		private const string MethodName = "SendAsync";

		public static IReturnsResult<HttpMessageHandler> SetupSendAsync(this Mock<HttpMessageHandler> mock,
			Uri requestUri, HttpMethod method, HttpStatusCode responseCode, string responseContent = null)
		{
			return mock.Protected()
				.Setup<Task<HttpResponseMessage>>(MethodName, GetArgs(requestUri, method))
				.ReturnsAsync(new HttpResponseMessage
				{
					StatusCode = responseCode,
					Content = responseContent != null ? new StringContent(responseContent) : null
				});
		}

		public static IReturnsResult<HttpMessageHandler> SetupThrowsOnSendAsync(this Mock<HttpMessageHandler> mock,
			Uri requestUri, HttpMethod method)
		{
			return mock.Protected()
				.Setup<Task<HttpResponseMessage>>(MethodName, GetArgs(requestUri, method))
				.ThrowsAsync(new HttpRequestException());
		}

		public static void Verify(this Mock<HttpMessageHandler> mock,
			Times times, Uri requestUri, HttpMethod method)
		{
			mock.Protected()
				.Verify<Task<HttpResponseMessage>>(MethodName, times, GetArgs(requestUri, method));
		}

		private static object[] GetArgs(Uri requestUri, HttpMethod method)
		{
			return new object[]
			{
				ItExpr.Is<HttpRequestMessage>(
					r => r.RequestUri == requestUri && r.Method == method),
				ItExpr.IsAny<CancellationToken>()
			};
		}
	}
}