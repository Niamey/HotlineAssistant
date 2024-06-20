using System;
using System.Collections.Generic;
using System.Net;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Core.Common.Models.HttpClientModels
{
	public class HttpClientResponseModel
	{
		public Uri RequestUrl { get; set; }
		public object Request { get; set; }

		public string Response { get; set; }
		public byte[] ResponseRaw { get; set; }
		public HttpStatusCode StatusCode { get; set; }
		public bool IsSuccessStatusCode { get; set; }

		public List<Cookie> Cookies { get; set; }

		public override string ToString()
		{
			return JsonHelper.ToJson(this, Newtonsoft.Json.Formatting.None);
		}
	}
}
