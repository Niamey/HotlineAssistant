using System;

namespace Vostok.Hotline.Core.Data.Exceptions
{
	internal class TransactionNotExistsException : ApplicationException
	{
		public TransactionNotExistsException(string message)
			: base(message)
		{
		}
	}
}
