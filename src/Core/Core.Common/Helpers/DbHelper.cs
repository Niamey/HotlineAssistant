using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class DbHelper
	{
		public static object GetDbValue(object value)
		{
			if (value == null)
				return DBNull.Value;

			return value;
		}
	}
}
