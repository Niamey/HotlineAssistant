using System;
using System.Collections.Generic;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Common.Response.Base;

namespace Vostok.HotLineAssistant.Common.Response.Common
{
	public class PageResponse<T> : BaseResponse
	{
		public virtual int Total { get; }

		public virtual int Size { get; }

		public int Current { get; }

		public int Last
		{
			get
			{
				var last = Total / Size;

				return Total % Size != 0 ? last + 1 : last;
			}
		}

		public IEnumerable<T> Items { get; }

		public PageResponse(PageOptions options, int total, params T[] items)
		{
			if (options == null)
				throw new ArgumentNullException(nameof(options));

			if (total < 0)
				throw new ArgumentOutOfRangeException(nameof(total));

			Size = options.Size;
			Current = options.Number;
			Total = total;
			Items = items;
		}
	}
}