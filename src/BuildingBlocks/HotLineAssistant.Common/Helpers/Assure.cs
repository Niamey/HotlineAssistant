using System;

namespace Vostok.HotLineAssistant.Common.Helpers
{
	public static class Assure
	{
		public static T ArgumentNotNull<T>(T value, string argumentName, string message = null)
		{
			if (value == null)
			{
				throw new ArgumentNullException(argumentName, message);
			}

			return value;
		}

		public static string ArgumentNotNullNotEmpty(string value, string argumentName, string message = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentNullException(argumentName, message);
			}

			return value;
		}
	}
}