using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Domain.Exceptions;
using Vostok.HotLineAssistant.WebApi.ActionResults;

namespace Vostok.HotLineAssistant.WebApi.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
        const string ProblemTitle = "An error has occured.";
        const string ProblemValidationDetail = "Please refer to the errors property for additional details.";

        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(IWebHostEnvironment env, ILogger<ExceptionFilter> logger)
        {
            _env = Assure.ArgumentNotNull(env, nameof(env));
            _logger = Assure.ArgumentNotNull(logger, nameof(logger));
        }

        public void OnException(ExceptionContext context)
        {
            LogLevel logError;

            switch (context.Exception)
            {
                case ValidationException validationException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = ActionResultFrom(validationException, context.HttpContext.Request.Path);
                    logError = LogLevel.Warning;
                    break;
                case DomainException domainException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = ActionResultFrom(domainException, context.HttpContext.Request.Path);
                    logError = LogLevel.Error;
                    break;
                case FailedRequestException failedRequestException:
	                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
	                context.Result = ActionResultFrom(failedRequestException, context.HttpContext.Request.Path);
	                logError = LogLevel.Error;
	                break;
                case AggregateDomainException aggregateDomainException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = ActionResultFrom(aggregateDomainException, context.HttpContext.Request.Path);
                    logError = LogLevel.Error;
                    break;
                case InvalidDomainOperationException invalidDomainOperationException:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    context.Result = ActionResultFrom(invalidDomainOperationException, context.HttpContext.Request.Path);
                    logError = LogLevel.Error;
                    break;
                default:
                    context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Result = ActionResultFrom(context.Exception, context.HttpContext.Request.Path);
                    logError = LogLevel.Critical;
                    break;
            }

            _logger.Log(logError, new EventId(context.Exception.HResult), context.Exception, context.Exception.Message);

            context.ExceptionHandled = true;
        }

        private IActionResult ActionResultFrom(Exception exception, string instance)
        {
            var problemDetails = new ProblemDetails
            {
                Title = ProblemTitle,
                Instance = instance,
                Status = StatusCodes.Status500InternalServerError,
                Detail = _env.IsDevelopment() ? exception.ToString() : null
            };

            return new ExceptionObjectResult(problemDetails);
        }

        private IActionResult ActionResultFrom(DomainException exception, string instance)
        {
            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Instance = instance,
                Status = StatusCodes.Status400BadRequest,
                Detail = _env.IsDevelopment() ? exception.ToString() : null
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private IActionResult ActionResultFrom(FailedRequestException exception, string instance)
        {
	        var problemDetails = new ProblemDetails
	        {
		        Title = exception.Message,
		        Instance = instance,
		        Status = StatusCodes.Status400BadRequest,
		        Detail = _env.IsDevelopment() ? exception.ToString() : null
	        };

	        return new BadRequestObjectResult(problemDetails);
        }
        private IActionResult ActionResultFrom(InvalidDomainOperationException exception, string instance)
        {
            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Instance = instance,
                Status = StatusCodes.Status400BadRequest,
                Detail = _env.IsDevelopment() ? exception.ToString() : null
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private IActionResult ActionResultFrom(AggregateDomainException exception, string instance)
        {
            var problemDetails = new ProblemDetails
            {
                Title = exception.Message,
                Instance = instance,
                Status = StatusCodes.Status400BadRequest,
                Detail = _env.IsDevelopment() ? exception.ToString() : null
            };

            return new BadRequestObjectResult(problemDetails);
        }

        private IActionResult ActionResultFrom(ValidationException exception, string instance)
        {
            var problemDetails = new ValidationProblemDetails
            {
                Title = ProblemTitle,
                Instance = instance,
                Status = StatusCodes.Status400BadRequest,
                Message = exception.Message,
                Detail = ProblemValidationDetail
            };

            FillValidationErrors(exception, problemDetails.Errors);

            return new BadRequestObjectResult(problemDetails);
        }

        private static void FillValidationErrors(ValidationException exception, IDictionary<string, object[]> validationErrors)
        {
            var temp = new Dictionary<string, List<object>>();
            foreach (var error in exception.Errors)
            {
                if (!temp.ContainsKey(error.PropertyName))
                    temp.Add(error.PropertyName, new List<object>());

                temp[error.PropertyName].Add(
                    error.FormattedMessagePlaceholderValues != null && error.FormattedMessagePlaceholderValues.ContainsKey(error.PropertyName)
                    ? error.FormattedMessagePlaceholderValues[error.PropertyName]
                    : new { message = error.ErrorMessage });
            }

            temp.Keys
                .ToList()
                .ForEach(p => validationErrors.Add(p, temp[p].ToArray()));
        }
    }

    public class ValidationProblemDetails : ProblemDetails
    {
        public string Message { get; set; }

        public IDictionary<string, object[]> Errors { get; } = new Dictionary<string, object[]>();

    }
}