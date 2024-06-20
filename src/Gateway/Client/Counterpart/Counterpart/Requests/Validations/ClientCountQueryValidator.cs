using FluentValidation;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Validations
{
	public class ClientCountQueryValidator : AbstractValidator<ClientCountQuery>
	{
		public ClientCountQueryValidator()
		{
			RuleFor(x => x.SearchFilter).NotEmpty();
			RuleFor(x => x.SearchFilter).MinimumLength(2).WithMessage("value must be at least 2 character long");
		}
	}
}