using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.WebApi.Authorization;

namespace Vostok.HotLineAssistant.WebApi
{
	public abstract class MediatrController : Controller
    {
	    protected readonly IMediator Mediator;
        protected readonly IAuthorizationService AuthorizationService;

        protected MediatrController(IServiceProvider provider)
        {
	        Mediator = provider.GetRequiredService<IMediator>();
	        AuthorizationService = provider.GetRequiredService<IAuthorizationService>();
        }
        
        protected async Task<IActionResult> ExecuteCommand(IRequest command, IAuthorizationRequirement requirement)
        {
            if (command == null)
                return BadRequest();

            if (!await IsRequestAuthorized(command, requirement))
                return Forbid();

            await Mediator.Send(command);

            return Ok();
        }

        protected async Task<IActionResult> ExecuteCreateCommand<TResponse>(IRequest<TResponse> command, IAuthorizationRequirement requirement,
            string getActionName = "Get", string getControllerName = null)
        {
            return await ExecuteCreateCommand(command, requirement, result =>
                getControllerName == null
                    ? CreatedAtAction(getActionName, new { id = result }, result)
                    : CreatedAtAction(getActionName, getControllerName, new { id = result }, result));
        }

        protected async Task<IActionResult> ExecuteCreateCommand<TResponse>(IRequest<TResponse> command, IAuthorizationRequirement requirement,
            Func<TResponse, CreatedAtActionResult> createAtAction)
        {
            if (command == null)
                return BadRequest();

            Assure.ArgumentNotNull(requirement, nameof(requirement));

            Assure.ArgumentNotNull(createAtAction, nameof(createAtAction));

            if (!await IsRequestAuthorized(command, requirement))
                return Forbid();

            var result = await Mediator.Send(command);

            if (Equals(result, default(TResponse)))
                return BadRequest();

            return createAtAction(result);
        }

        protected async Task<IActionResult> ExecuteQuery<T>(IRequest<T> query, QueryAuthorizationSettings authorization)
	        where T : class
        {
	        Assure.ArgumentNotNull(authorization, nameof(authorization));

	        if (query == null)
		        return BadRequest();

	        if (authorization.IsQueryAuthorizationRequired &&
	            !await IsRequestAuthorized(query, authorization.Requirement))
		        return Forbid();

	        var entity = await Mediator.Send(query);
	        if (entity == null)
		        return NotFound();

	        if (authorization.IsResourceAuthorizationRequired &&
	            !await IsResourceAccessAuthorized(entity, authorization.Requirement))
		        return Forbid();

	        return Json(entity);
        }

        protected async Task<IActionResult> Download(IRequest<Stream> query, string contentType, string downloadName, IAuthorizationRequirement requirement)
        {
            if (query == null)
                return BadRequest();

            Assure.ArgumentNotNull(requirement, nameof(requirement));

            if (!await IsRequestAuthorized(query, requirement))
                return Forbid();

            var stream = await Mediator.Send(query);
            if (stream == null)
                return NotFound();

            return new FileStreamResult(stream, contentType)
            {
                FileDownloadName = downloadName
            };
        }

        private async Task<bool> IsRequestAuthorized<T>(IRequest<T> request, IAuthorizationRequirement requirement)
        {
            var result = await AuthorizationService.AuthorizeAsync(User, request, requirement);

            return result.Succeeded;
        }

        private async Task<bool> IsResourceAccessAuthorized(object resource, IAuthorizationRequirement requirement)
        {
            var result = await AuthorizationService.AuthorizeAsync(User, resource, requirement);

            return result.Succeeded;
        }
    }
}