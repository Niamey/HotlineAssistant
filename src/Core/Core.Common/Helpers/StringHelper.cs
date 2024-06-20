using System;
using System.Linq;

namespace Vostok.Hotline.Core.Common.Helpers
{
	public static class StringHelper
	{
		public static string GetOnlyDigits(string value) 
		{
			return new String(value.Where(Char.IsDigit).ToArray());
		}

		public static string GetOnlyLetters(string value)
		{
			return new String(value.Where(Char.IsLetter).ToArray());
		}
	}
}
