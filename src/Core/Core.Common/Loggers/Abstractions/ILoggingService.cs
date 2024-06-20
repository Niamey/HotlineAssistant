using System;
using System.Runtime.CompilerServices;

namespace Vostok.Hotline.Core.Common.Loggers.Abstractions
{
	public interface ILoggingService
	{
		void Error(string message);
		void Information(string message);

		void LogInformation(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogInformation(string message,	object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogError(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogError(string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogError(out Guid exceptionUid, Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogError(Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogCritical(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogCritical(string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogCritical(out Guid exceptionUid, Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogCritical(Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogDebug(out Guid exceptionUid, Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogDebug(Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogDebug(string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogDebug(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogTrace(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogTrace(string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogTrace(out Guid exceptionUid, Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogTrace(Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogWarning(string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogWarning(out Guid exceptionUid, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");

		void LogWarning(Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
		void LogWarning(out Guid exceptionUid, Exception exception, string message, object obj = null,
				[CallerMemberName] string memberName = "",
				[CallerLineNumber] int sourceLineNumber = 0,
				[CallerFilePath] string sourceFilePath = "");
	}

	public interface ILoggingService<out T> : ILoggingService
	{

	}
}
