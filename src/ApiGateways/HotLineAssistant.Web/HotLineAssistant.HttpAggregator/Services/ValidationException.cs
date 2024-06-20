using System;

namespace Vostok.HotLineAssistant.HttpAggregator.Services
{
	public class ValidationException : Exception
	{
		public ValidationException()
		{
		}

		public ValidationException(string message)
			: base(message)
		{
		}

		public ValidationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		public object Errors { get; set; }
	}
}