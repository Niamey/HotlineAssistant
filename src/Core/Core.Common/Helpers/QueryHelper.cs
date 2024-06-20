using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class QueryHelper
	{
		public static string ToValue(DateTime? dateTime)
		{
			// return ISO-8601
			return dateTime?.ToString("O") ?? "";
		}
	}
}
