using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Net.Http;
using Vostok.HotLineAssistant.HttpAggregator.Services;

namespace Vostok.HotLineAssistant.HttpAggregator.Infrastructure.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        const string ProblemTitle = "An error has occured.";

        public void OnException(ExceptionContext context)
        {
            if (HandleValidationException(context)) return;

            context.ExceptionHandled = true;

            if (context.Exception.GetType() == typeof(HttpRequestException))
            {
                var message = context.Exception.Message;

                if (message.Contains("404"))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                    context.Result = new NotFoundResult();
                    return;
                }

                if (message.Contains("401"))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Result = new UnauthorizedResult();
                    return;
                }

                if (message.Contains("403"))
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Result = new ForbidResult();
                    return;
                }
            }

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = ActionResultFrom(context.Exception, context.HttpContext.Request.Path);
        }

        private IActionResult ActionResultFrom(Exception exception, string instance)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ProblemTitle,
                Instance = instance,
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.ToString()
            };

            return new ObjectResult(problemDetails);
        }

        private static bool HandleValidationException(ExceptionContext context)
        {
            if (context.Exception.GetType() != typeof(ValidationException)) return false;

            var errors = ((ValidationException)context.Exception).Errors;

            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.HttpContext.Response.ContentType = "application/json";
            context.Result = new BadRequestObjectResult(errors);

            return true;
        }
    }
}