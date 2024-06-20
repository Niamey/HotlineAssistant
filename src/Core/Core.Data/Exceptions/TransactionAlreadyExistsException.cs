using System;

namespace Vostok.Hotline.Core.Data.Exceptions
{
	public class TransactionAlreadyExistsException : ApplicationException
	{
		public TransactionAlreadyExistsException(string message)
			: base(message)
		{
		}
	}
}
