using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Core.Common.Exeptions
{
	public class FailedRequestException : Exception
	{
		public int StatusCode { get; set; }

		public FailedRequestException(string message, Exception innerException = null)
			: base(message, innerException)
		{
			StatusCode = StatusCodes.Status500InternalServerError;
		}

		public FailedRequestException(HttpClientResponseModel responseModel)
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