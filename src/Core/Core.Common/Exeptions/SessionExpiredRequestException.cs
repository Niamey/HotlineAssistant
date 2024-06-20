using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Exeptions
{
	public class SessionExpiredRequestException : Exception
	{
		public int StatusCode { get; } = StatusCodes.Status401Unauthorized;

		public SessionExpiredRequestException()
			: base("Session expired")
		{
		}

		public SessionExpiredRequestException(string message)
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