using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Api.Services.Exceptions
{
	public class MessengerApiException: Exception
	{
		public int StatusCode { get; } = StatusCodes.Status500InternalServerError;

		public MessengerApiException()
			: base("MessengerError")
		{
		}

		public MessengerApiException(string message, Exception innerException = null)
			: base(message, innerException)
		{
		}

		public MessengerApiException(HttpClientResponseModel responseModel)
			: base($"HTTP request error {responseModel}")
		{
		}

		public override string ToString()
		{
			var exception = new
			{
				StatusCode,
				Message
			};

			return JsonHelper.ToJson(exception, Newtonsoft.Json.Formatting.None);
		}
	}
}
