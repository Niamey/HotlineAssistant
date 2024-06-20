using System;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Exeptions
{
	public class NotFoundRequestException : Exception
	{
		public int StatusCode { get; } = StatusCodes.Status404NotFound;

		public NotFoundRequestException()
			: base("Data not found")
		{
		}

		public NotFoundRequestException(string message)
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