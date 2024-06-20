using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.WebApi.ModelBinders
{
	public class CommandModelBinderProvider : IModelBinderProvider
	{
		private readonly BodyModelBinderProvider _bodyModelBinderProvider;

		public CommandModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory)
			: this(formatters, readerFactory, null)
		{
		}

		public CommandModelBinderProvider(IList<IInputFormatter> formatters, IHttpRequestStreamReaderFactory readerFactory, ILoggerFactory loggerFactory)
			: this(formatters, readerFactory, loggerFactory, null)
		{
		}

		public CommandModelBinderProvider(
			IList<IInputFormatter> formatters,
			IHttpRequestStreamReaderFactory readerFactory,
			ILoggerFactory loggerFactory,
			MvcOptions options)
		{
			_bodyModelBinderProvider = new BodyModelBinderProvider(formatters, readerFactory, loggerFactory, options);
		}

		public IModelBinder GetBinder(ModelBinderProviderContext context)
		{
			Assure.ArgumentNotNull(context, nameof(context));

			var bindingSource = context.BindingInfo.BindingSource;

			return bindingSource != null && bindingSource.CanAcceptDataFrom(BindingSource.Body)
				? new CommandModelBinder(_bodyModelBinderProvider.GetBinder(context))
				: null;
		}
	}
}