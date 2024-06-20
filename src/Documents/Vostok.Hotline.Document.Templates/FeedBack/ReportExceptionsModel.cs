using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Documents.Abstractions;

namespace Vostok.Hotline.Document.Templates.FeedBack
{
	public class ReportExceptionsModel : ITemplateViewModel
	{
		public Guid SessionId { get; set; }
		public HttpRequest Request { get; set; }
		public object[] Exceptions { get; set; }
		public string RequestedUrl { get; set; }

		public override string ToString()
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("<h3>RequestedUrl:</h3>");
			result.AppendLine($"{RequestedUrl}");
			result.AppendLine();

			if (!string.IsNullOrEmpty(SessionId.ToString()))
			{
				result.AppendLine("<h3>SessionID:</h3>");
				result.AppendLine($"{SessionId}");
				result.AppendLine();
			}

			result.AppendLine("<h3>User-Agent:</h3>");
			result.AppendLine($"{ Request.Headers["User-Agent"] }");
			result.AppendLine();


			if (Request.Cookies.Count > 0) {
				result.AppendLine("<h3>Cookies:</h3>");
				result.AppendLine();
				result.AppendLine(JsonHelper.ToJson(Request.Cookies, Newtonsoft.Json.Formatting.Indented));
			}

			result.AppendLine("<h3>Exceptions:</h3>");
			result.AppendLine();
			result.AppendLine(JsonHelper.ToJson(Exceptions, Newtonsoft.Json.Formatting.Indented));
			return result.ToString();
		}
	}
}
