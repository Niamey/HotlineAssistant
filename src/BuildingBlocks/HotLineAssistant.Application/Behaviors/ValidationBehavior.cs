using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.Application.Behaviors
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly IValidator<TRequest>[] _validators;

		public ValidationBehavior(IValidator<TRequest>[] validators)
		{
			_validators = validators;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var failures = _validators
				.Select(v => v.Validate(request))
				.SelectMany(r => r.Errors)
				.ToList();

			if (failures.Any())
			{
				throw new ValidationException("Validation exception", failures);
			}

			return await next();
		}
	}
}