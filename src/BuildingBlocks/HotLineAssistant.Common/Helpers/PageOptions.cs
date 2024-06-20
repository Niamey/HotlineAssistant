using System;
using Vostok.HotLineAssistant.Common.Requests.Common;

namespace Vostok.HotLineAssistant.Common.Helpers
{
	public class PageOptions
	{
		public const int DefaultCurrent = 1;

		public const int DefaultSize = 10;

		public static readonly PageOptions Default = new PageOptions(DefaultCurrent, DefaultSize);

		public int Number { get; }

		public int Size { get; }

		public PageOptions(int number, int size)
		{
			if (number <= 0)
				throw new ArgumentOutOfRangeException(nameof(number));

			if (size <= 0)
				throw new ArgumentOutOfRangeException(nameof(size));

			Number = number;
			Size = size;
		}

		public static PageOptions From(ByPageRequest request)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			return new PageOptions(request.PageNumber ?? DefaultCurrent, request.PageSize ?? DefaultSize);
		}
	}
}