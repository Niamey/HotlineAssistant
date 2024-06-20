using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Models.HttpClientModels;

namespace Vostok.Hotline.Core.Common.Exeptions
{
	public class RetailApiFailedException : FailedRequestException
	{
		public RetailApiFailedException(Exception innerException = null)
			: base("RetailApiError", innerException)
		{
			StatusCode = StatusCodes.Status500InternalServerError;
		}

		public RetailApiFailedException(HttpClientResponseModel responseModel)
			: base("RetailApiError")
		{
		}
	}
}
