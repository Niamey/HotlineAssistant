using FluentValidation;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Queries
{
	public class ClientSearchQueryValidator : AbstractValidator<ClientSearchQuery>
	{
		public ClientSearchQueryValidator()
		{
			RuleFor(x => x.Code).NotEmpty();
			RuleFor(x => x.Criteria).NotEmpty();
		}
	}
}