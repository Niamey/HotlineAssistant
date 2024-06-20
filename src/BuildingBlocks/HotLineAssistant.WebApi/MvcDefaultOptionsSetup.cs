using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.WebApi.Filters;
using Vostok.HotLineAssistant.WebApi.ModelBinders;

namespace Vostok.HotLineAssistant.WebApi
{
	public class MvcDefaultOptionsSetup : IConfigureOptions<MvcOptions>
	{
		private readonly IHttpRequestStreamReaderFactory _readerFactory;
		private readonly ILoggerFactory _loggerFactory;

		public MvcDefaultOptionsSetup(IHttpRequestStreamReaderFactory readerFactory)
			: this(readerFactory, NullLoggerFactory.Instance)
		{
		}

		public MvcDefaultOptionsSetup(IHttpRequestStreamReaderFactory readerFactory, ILoggerFactory loggerFactory)
		{
			_readerFactory = Assure.ArgumentNotNull(readerFactory, nameof(readerFactory));
			_loggerFactory = Assure.ArgumentNotNull(loggerFactory, nameof(loggerFactory));
		}

		public virtual void Configure(MvcOptions options)
		{
			Assure.ArgumentNotNull(options, nameof(options));

			options.Filters.Add(typeof(ExceptionFilter));
			options.ModelBinderProviders.Insert(0, new CommandModelBinderProvider(options.InputFormatters, _readerFactory, _loggerFactory, options));
		}
	}
}