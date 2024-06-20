using System;
using System.Collections.Generic;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public class OperationLogService : IOperationLogService
	{
		public IEnumerable<CardOperationLog> GetLastOperations()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<CardOperationLog> GetOperationsByPeriod(DateTime startDate, DateTime? endDate)
		{
			throw new NotImplementedException();
		}
	}
}