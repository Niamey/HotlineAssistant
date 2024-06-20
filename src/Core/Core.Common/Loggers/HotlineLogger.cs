using System;
using System.Linq;
using System.Runtime.CompilerServices;
using NLog;
using Vostok.Hotline.Core.Common.Base.Abstractions;
using Vostok.Hotline.Core.Common.Exeptions.Abstractions;
using Vostok.Hotline.Core.Common.Helpers;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;
using Vostok.Hotline.Core.Common.Security;

namespace Vostok.Hotline.Core.Common.Loggers
{
	public class HotlineLogger<T> : ILoggingService<T>
	{
		protected readonly static NLog.ILogger logger = NLog.LogManager.GetLogger(typeof(T).FullName);
		protected Lazy<ISessionContext> sessionContext;
		protected Lazy<UserSession> userSession;

		public HotlineLogger(IServiceProvider serviceProvider)
		{
			sessionContext = new Lazy<ISessionContext>(() => serviceProvider.GetService(typeof(ISessionContext)) as ISessionContext);
			userSession = new Lazy<UserSession>(() => serviceProvider.GetService(typeof(UserSession)) as UserSession);
		}

		private LogEventInfo GetFormatMessage(Guid exceptionUid, LogLevel logLevel, string message, string memberName, int sourceLineNumber, string sourceFilePath, object obj = null)
		{
			return GetFormatMessage(exceptionUid, logLevel, message, memberName, sourceLineNumber, sourceFilePath, null, obj);
		}

		private LogEventInfo GetFormatMessage(Guid exceptionUid, LogLevel logLevel, string message, string memberName, int sourceLineNumber, string sourceFilePath, Exception ex = null, object obj = null)
		{
			var newLineSymbols = Environment.NewLine.ToCharArray();
			if (newLineSymbols.Any(x => message.Contains(x)))
			{
				message = JsonHelper.ToJson(message, Newtonsoft.Json.Formatting.None);
			}

			var logEvent = new LogEventInfo(logLevel, logger.Name, message);

			if(sessionContext.Value.SessionId != Guid.Empty)
				logEvent.Properties["session-id"] = sessionContext.Value.SessionId;

			if (userSession.Value?.Authorized ?? false)
				logEvent.Properties["user-login"] = userSession.Value.CurrentUser.Login;

			if (ex != null)
			{
				logEvent.Exception = ex;
				logEvent.Properties["exception-details"] = JsonHelper.ToJson(ex);
			}

			logEvent.Properties["log-exception-uid"] = exceptionUid;

			var extended = new {
				extendedObject = obj,
				callerMethodName = memberName,
				sourceFilePath = sourceFilePath,
				sourceLineNumber = sourceLineNumber,
				//metaData = sessionContext.Value.GetMetadata()
			};

			logEvent.Properties["extended-object"] = JsonHelper.ToJson(extended);

			return logEvent;
		}

		public void Information(string message)
		{
			if (!logger.IsInfoEnabled)
				return;

			logger.Info(message);
		}

		public void Error(string message)
		{
			if (!logger.IsErrorEnabled)
				return;

			logger.Error(message);
		}

		public void LogInformation(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogInformation(out Guid exceptionUid, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogInformation(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			exceptionUid = Guid.NewGuid();
			if (!logger.IsInfoEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Info, message, memberName, sourceLineNumber, sourceFilePath, obj);

			logger.Log(logEvent);
		}

		public void LogError(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogError(out exceptionUid, null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogError(out Guid exceptionUid, Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			if (exception != null && exception is IExceptionUid)
				exceptionUid = ((IExceptionUid)exception).ExceptionUid;
			else
				exceptionUid = Guid.NewGuid();

			if (!logger.IsErrorEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Error, message, memberName, sourceLineNumber, sourceFilePath, exception, obj);

			logger.Log(logEvent);
		}

		public void LogError(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogError(null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogError(Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogError(out Guid exceptionUid, exception, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogCritical(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogCritical(null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogCritical(Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogCritical(out Guid exceptionUid, exception, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogCritical(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogCritical(out exceptionUid, null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogCritical(out Guid exceptionUid, Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			if (exception != null && exception is IExceptionUid)
				exceptionUid = ((IExceptionUid)exception).ExceptionUid;
			else
				exceptionUid = Guid.NewGuid();

			if (!logger.IsFatalEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Fatal, message, memberName, sourceLineNumber, sourceFilePath, exception, obj);

			logger.Log(logEvent);
		}

		public void LogDebug(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogDebug(null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogDebug(Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogDebug(out Guid exceptionUid, exception, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogDebug(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogDebug(out exceptionUid, null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogDebug(out Guid exceptionUid, Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			if (exception != null && exception is IExceptionUid)
				exceptionUid = ((IExceptionUid)exception).ExceptionUid;
			else
				exceptionUid = Guid.NewGuid();

			if (!logger.IsDebugEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Debug, message, memberName, sourceLineNumber, sourceFilePath, exception, obj);

			logger.Log(logEvent);
		}

		public void LogTrace(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogTrace(null, message,  obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogTrace(Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogTrace(out Guid exceptionUid, exception, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogTrace(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogTrace(out exceptionUid, null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogTrace(out Guid exceptionUid, Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			if (exception != null && exception is IExceptionUid)
				exceptionUid = ((IExceptionUid)exception).ExceptionUid;
			else
				exceptionUid = Guid.NewGuid();

			if (!logger.IsTraceEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Trace, message, memberName, sourceLineNumber, sourceFilePath, exception, obj);

			logger.Log(logEvent);
		}

		public void LogWarning(string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogWarning(null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogWarning(Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogWarning(out Guid exceptionUid, exception, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogWarning(out Guid exceptionUid, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			LogWarning(out exceptionUid, null, message, obj, memberName, sourceLineNumber, sourceFilePath);
		}

		public void LogWarning(out Guid exceptionUid, Exception exception, string message, object obj = null, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0, [CallerFilePath] string sourceFilePath = "")
		{
			if (exception != null && exception is IExceptionUid)
				exceptionUid = ((IExceptionUid)exception).ExceptionUid;
			else
				exceptionUid = Guid.NewGuid();

			if (!logger.IsWarnEnabled)
				return;

			var logEvent = GetFormatMessage(exceptionUid, LogLevel.Warn, message, memberName, sourceLineNumber, sourceFilePath, exception, obj);

			logger.Log(logEvent);
		}
	}
}
