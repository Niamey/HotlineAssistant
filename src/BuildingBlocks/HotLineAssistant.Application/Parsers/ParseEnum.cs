using System;

namespace Vostok.HotLineAssistant.Application.Parsers
{
	public static class ParseEnum
	{
		public static T Parse<T>(string value)
		{
			return (T)Enum.Parse(typeof(T), value, true);
		}
	}
}