using System.Collections.Generic;

namespace Vostok.Hotline.Storefront.Models.FeedBack
{
	public class Request
	{
		public string Url { get; set; }
		public string HttpMethod { get; set; }
		public Dictionary<string, object> RequestParams { get; set; }
	}
}
