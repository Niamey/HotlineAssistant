using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class DateTimeFormatterHelper
	{
		public static string ToGeneralLongString(DateTime? value)
		{
			return value?.ToLocalTime().ToString("G") ?? "";
		}
	}
}
