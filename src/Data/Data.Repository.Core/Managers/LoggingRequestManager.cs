using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Data.EF.Entities.Core;
using Vostok.Hotline.Data.EF.Enums;
using Vostok.Hotline.Data.Repository.Core.Managers.Base;

namespace Vostok.Hotline.Data.Repository.Core.Managers
{
	public class LoggingRequestManager : BaseCoreManager
	{
		
		public LoggingRequestManager(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			
		}

		public async Task AddAsync(LoggingSystemTypeEnum loggingSystemType, LoggingOperationTypeEnum? loggingOperationType,  object requestValue, object responseValue, CancellationToken cancellationToken, string uniqueRequestId = null)
		{
			await AddAsync(SessionContext.SessionId, loggingSystemType, loggingOperationType, hasError: false, requestValue, responseValue, cancellationToken, uniqueRequestId);
		}

		public async Task AddAsync(LoggingSystemTypeEnum loggingSystemType, LoggingOperationTypeEnum? loggingOperationType, object requestValue, Exception exception, CancellationToken cancellationToken, string uniqueRequestId = null)
		{
			await AddAsync(SessionContext.SessionId, loggingSystemType, loggingOperationType, hasError: true, requestValue, exception, cancellationToken, uniqueRequestId);
		}

		public async Task AddAsync(LoggingSystemTypeEnum loggingSystemType, LoggingOperationTypeEnum? loggingOperationType, object requestValue, object responseValue, CancellationToken cancellationToken)
		{
			await AddAsync(SessionContext.SessionId, loggingSystemType, loggingOperationType, hasError: false, requestValue, responseValue, cancellationToken);
		}

		public async Task AddAsync(LoggingSystemTypeEnum loggingSystemType, LoggingOperationTypeEnum? loggingOperationType, object requestValue, Exception exception, CancellationToken cancellationToken)
		{
			await AddAsync(SessionContext.SessionId, loggingSystemType, loggingOperationType, hasError: true, requestValue, exception, cancellationToken);
		}

		protected async Task AddAsync(Guid sessionId, LoggingSystemTypeEnum loggingSystemType, LoggingOperationTypeEnum? loggingOperationType, bool hasError, object requestValue, object responseValue, CancellationToken cancellationToken, string uniqueRequestId = null)
		{
			var loggingRequest = new LoggingRequest()
			{
				SessionId = sessionId,
				UniqueRequestId = uniqueRequestId,
				LoggingSystemType = loggingSystemType,
				LoggingOperationType = loggingOperationType,
				HasError = hasError,
				RequestValue = requestValue != null ? JsonHelper.ToJson(requestValue) : null,
				ResponseValue = responseValue != null ? JsonHelper.ToJson(responseValue) : null
			};

			using (var transaction = TransactionFactory.Create())
			{
				DbContext.LoggingRequests.Add(loggingRequest);
				var res = await DbContext.SaveChangesAsync(SessionContext, cancellationToken);
				transaction.Complete();
			}
		}

	}
}
