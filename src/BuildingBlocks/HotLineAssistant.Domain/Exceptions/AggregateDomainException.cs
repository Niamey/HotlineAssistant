using System;
using System.Collections.Generic;

namespace Vostok.HotLineAssistant.Domain.Exceptions
{
	public class AggregateDomainException : AggregateException
	{
		private const string DefaultMessage = "One or more errors occured.";

		public AggregateDomainException() : base(DefaultMessage)
		{
		}

		public AggregateDomainException(string message) : base(message)
		{
		}

		public AggregateDomainException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public AggregateDomainException(string message, IEnumerable<Exception> exceptions)
			: base(message, exceptions)
		{
		}

		public AggregateDomainException(string message, params Exception[] exceptions)
			: base(message, exceptions)
		{
		}

		public AggregateDomainException(IEnumerable<Exception> exceptions)
			: base(DefaultMessage, exceptions)
		{
		}

		public AggregateDomainException(params Exception[] exceptions)
			: base(DefaultMessage, exceptions)
		{
		}
	}
}