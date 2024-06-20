using System;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class CardHelper
	{
		public static string GetMaskedCardNumber(string value)
		{
			return GetMaskedCardNumber(value, '●');
		}

		public static string GetMaskedCardNumber(string value, char maskSymbol)
		{
			if (string.IsNullOrEmpty(value))
				return value;

			if (value.Length == 16)
				return $"{value.Substring(0, 4)} {value.Substring(4, 2)}{new String(maskSymbol, 2)} {new String(maskSymbol, 4)} {value.Substring(12, 4)}";
			else
				return value;
		}
	}
}
