using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Storefront.Models.FeedBack
{
	public class ReportedError
	{
		public string SessionId { get; set; }

		public int Status { get; set; }

		public string  StatusCode { get; set; }

		public string Message { get; set; }

		public string Exception { get; set; }

		public Request Request { get; set; }

	}
}
