using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Data.Exceptions;

namespace Vostok.Hotline.Core.Data.Helpers
{
	internal class EfTransactionHelper
	{
		/// <summary>
		/// Verify the current transaction does not exists, otherwise throw an error
		/// </summary>
		/// <exception cref="TransactionAlreadyExistsException" />
		public static void EnsureTransactionNotExists(string message = null)
		{
			if (System.Transactions.Transaction.Current != null)
			{
				throw new TransactionAlreadyExistsException(message ?? "The action can not be executed inside transaction");
			}
		}

		/// <summary>
		/// Verify the current transaction is exists, otherwise throw an error
		/// </summary>
		/// <exception cref="TransactionNotExistsException" />
		public static void EnsureTransactionExists(string message = null)
		{
			if (System.Transactions.Transaction.Current == null)
			{
				throw new TransactionNotExistsException(message ?? "The action must be executed inside transaction");
			}
		}

		public static bool IsTransactionExists()
		{
			return System.Transactions.Transaction.Current != null;
		}
	}
}
