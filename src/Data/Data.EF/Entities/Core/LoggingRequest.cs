using System;
using Vostok.Hotline.Core.Data.Entities;
using Vostok.Hotline.Data.EF.Enums;

namespace Vostok.Hotline.Data.EF.Entities.Core
{
	public class LoggingRequest : BusinessEntityBase
	{
		public LoggingRequest()
		{
		}

		public int LoggingRequestId { get; set; }

		public string UniqueRequestId { get; set; }

		public LoggingSystemTypeEnum LoggingSystemType { get; set; }

		public LoggingOperationTypeEnum? LoggingOperationType { get; set; }

		public bool HasError { get; set; }

		public string RequestValue { get; set; }

		public string ResponseValue  { get; set; }
	}
}
