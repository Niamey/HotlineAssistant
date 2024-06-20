using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.WebApi.ModelBinders
{
	public class CommandModelBinder : IModelBinder
	{
		private readonly IModelBinder _innerBinder;

		public CommandModelBinder(IModelBinder innerBinder)
		{
			_innerBinder = Assure.ArgumentNotNull(innerBinder, nameof(innerBinder));
		}

		public async Task BindModelAsync(ModelBindingContext bindingContext)
		{
			Assure.ArgumentNotNull(bindingContext, nameof(bindingContext));

			await _innerBinder.BindModelAsync(bindingContext);

			if (bindingContext.Result == ModelBindingResult.Failed())
				return;

			foreach (var (key, value) in bindingContext.ActionContext.RouteData.Values)
			{
				SetRouteValue(bindingContext, key, value);
			}
		}

		private void SetRouteValue(ModelBindingContext bindingContext, string key, object value)
		{
			var model = bindingContext.Result.Model;
			var property = model.GetType().GetProperty(key);
			if (property == null || !property.CanWrite)
				return;

			try
			{
				property.SetValue(model, TypeDescriptor.GetConverter(property.PropertyType).ConvertFrom(value));
			}
			catch (Exception e)
			{
				if (!(e is FormatException) && e.InnerException != null)
					e = ExceptionDispatchInfo.Capture(e.InnerException).SourceException;

				bindingContext.ModelState.TryAddModelException(key, e);
				bindingContext.Result = ModelBindingResult.Failed();
			}
		}
	}
}