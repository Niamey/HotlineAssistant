using System;

namespace Vostok.HotLineAssistant.Domain.Exceptions
{
	public class InvalidDomainOperationException : InvalidOperationException
	{
		public InvalidDomainOperationException()
		{
		}

		public InvalidDomainOperationException(string message) : base(message)
		{
		}

		public InvalidDomainOperationException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}
