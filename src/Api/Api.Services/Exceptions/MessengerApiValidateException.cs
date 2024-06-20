using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Api.Services.Exceptions
{
	public class MessengerApiValidateException: Exception
	{
		public int StatusCode { get; } = StatusCodes.Status400BadRequest;

		public MessengerApiValidateException()
			: base("MessagePackageIsIncorrect")
		{
		}

		public MessengerApiValidateException(string message)
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
