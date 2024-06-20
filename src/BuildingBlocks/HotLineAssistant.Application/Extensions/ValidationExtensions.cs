using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Vostok.HotLineAssistant.Application.Extensions
{
	public static class ValidationExtensions
	{
		public static IRuleBuilderOptions<T, TProperty> WithMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, IEnumerable<string> options)
		{
			return rule.WithMessage($"Expected values are {options.Aggregate((a, b) => a + ", " + b).TrimStart(',')}");
		}

		public static IRuleBuilderOptions<T, int> MustBeYear<T>(this IRuleBuilder<T, int> rule)
		{
			return rule.Must(c => DateTime.MinValue.Year <= c && DateTime.MaxValue.Year >= c)
				.WithMessage("Invalid year value.");
		}

		public static ValidationException ExceptionForCommandHandler(string propertyName,
			string errorMessage, object attemptedValue = null)
		{
			throw new ValidationException(
				"Failed to handle command.",
				new[] { new ValidationFailure(propertyName, errorMessage, attemptedValue) });
		}
	}
}