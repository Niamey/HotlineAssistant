using FluentValidation;
using Vostok.Hotline.Authorization.Requests.Commands;

namespace Vostok.Hotline.Authorization.Requests.Validations
{
	public class LoginCommandValidator : AbstractValidator<LoginCommand>
	{
		public LoginCommandValidator()
		{
			RuleFor(x => x.Login).NotEmpty();
			RuleFor(x => x.Password).NotEmpty();
		}
	}
}