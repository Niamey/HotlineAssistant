using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Loggers.Abstractions;

namespace Vostok.Hotline.Core.Common.Base.Controllers
{
	//[TypeFilter(typeof(HotlineAuthorizeAttribute))]
	public class MediatrControllerBase : CoreControllerBase
	{
		protected readonly Lazy<ILoggingService> logger;
		protected readonly IMediator mediator;

		protected MediatrControllerBase(IServiceProvider provider)
		{
			logger = new Lazy<ILoggingService>(() =>
			{
				var ctrlType = this.GetType();
				var loggerType = typeof(ILoggingService<>).MakeGenericType(ctrlType);

				return this.HttpContext.RequestServices.GetService(loggerType) as ILoggingService;
			});

			mediator = provider.GetRequiredService<IMediator>();
		}

		protected async Task<IActionResult> ExecuteQueryAsync<T>(IRequest<T> query)
			where T : IResponseModel
		{
			if (query == null)
			{
				logger.Value.LogWarning($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is BadRequest.", new { query });

				return BadRequest();
			}

			var entity = await mediator.Send(query);
			if (entity == null)
			{
				logger.Value.LogWarning($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is NotFound.", new { query });

				return NotFound();
			}
			logger.Value.LogInformation($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is success.", new { query, result=entity });

			return Ok(entity);
		}

		protected async Task<T> ExecuteQueryModelAsync<T>(IRequest<T> query)
		{
			var entity = await mediator.Send(query);
			return entity;
		}

		protected async Task<IActionResult> ExecuteQueryAsync(IRequest query)
		{
			if (query == null)
			{
				logger.Value.LogWarning($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is BadRequest.", new { query });

				return BadRequest();
			}

			var entity = await mediator.Send(query);
			if (entity == null)
			{
				logger.Value.LogWarning($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is NotFound.", new { query });

				return NotFound();
			}
			logger.Value.LogInformation($"{this.ControllerContext.ActionDescriptor.DisplayName} {nameof(ExecuteQueryAsync)}. Request is success.", new { query, result = entity });

			return Ok(entity);
		}
	}
}
