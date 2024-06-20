using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Queries.Modules;
using Vostok.Hotline.Storefront.Services.Menu;
using Vostok.Hotline.Storefront.ViewModels.Modules;

namespace Vostok.Hotline.Storefront.Requests.Handlers.SearchLinks
{
	public class PersonalDataConfigurationQueryHandler : IRequestHandler<PersonalDataConfigurationQuery, SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>>
	{
		private readonly PersonalDataService _personalDataService;

		public PersonalDataConfigurationQueryHandler(PersonalDataService personalDataService)
		{
			_personalDataService = personalDataService;
		}

		public async Task<SearchRowsResponseRowModel<PersonalDataConfigurationViewModel>> Handle(PersonalDataConfigurationQuery request, CancellationToken cancellationToken)
		{
			return await _personalDataService.GetConfigurationAsync(request.Localization.Value, cancellationToken);
		}
	}
}
