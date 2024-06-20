using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.Application.Behaviors
{
	public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

		public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
		{
			_logger = Assure.ArgumentNotNull(logger, nameof(logger));
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			Log("Handling");

			var response = await next();

			Log("Handled");

			return response;
		}

		private void Log(string message)
		{
			_logger.LogInformation($"{message} {typeof(TRequest).Name}".TrimStart());
		}
	}
}