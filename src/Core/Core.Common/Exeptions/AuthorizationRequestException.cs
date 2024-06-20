using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Exeptions
{
	public class AuthorizationRequestException : Exception
	{
		public int StatusCode { get; } = StatusCodes.Status500InternalServerError;

		public AuthorizationRequestException()
			: base("Authorization is incorrect")
		{
		}

		public AuthorizationRequestException(string message)
			: base(message)
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