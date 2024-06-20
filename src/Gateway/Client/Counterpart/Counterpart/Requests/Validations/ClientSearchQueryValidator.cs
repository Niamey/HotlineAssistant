using FluentValidation;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Validations
{
	public class ClientSearchQueryValidator : AbstractValidator<ClientSearchQuery>
	{
		public ClientSearchQueryValidator()
		{
			RuleFor(x => x.SearchFilter).NotEmpty();
			RuleFor(x => x.SearchFilter).MinimumLength(2).WithMessage("value must be at least 2 character long");
			RuleFor(x => x.PageSize).GreaterThan(1).WithMessage("must be at least 0 character long");
		}
	}
}