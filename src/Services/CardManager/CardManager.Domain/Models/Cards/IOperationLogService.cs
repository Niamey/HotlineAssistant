using System;
using System.Collections.Generic;

namespace Vostok.HotLineAssistant.CardManager.Domain.Models.Cards
{
	public interface IOperationLogService
	{
		public IEnumerable<CardOperationLog> GetLastOperations();
		public IEnumerable<CardOperationLog> GetOperationsByPeriod(DateTime startDate, DateTime? endDate);
	}
}