using System;

namespace Vostok.HotLineAssistant.Domain.Exceptions
{
	public class FailedRequestException : Exception
	{
		public FailedRequestException() {}
		public FailedRequestException(string message) : base(message)
		{
		}

		public FailedRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}