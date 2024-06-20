using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client
{
	public class PagedResponseEx<T> : PageResponse<T>
	{
		//public List<T> Data { get; set; }
		//public int Page { get; set; }
		//public int Total { get; set; }
		public bool IsNextPageAvailable { get; set; }

		public PagedResponseEx(PageOptions options, int total, params T[] items) : base(options, total, items)
		{
		}
	}
}