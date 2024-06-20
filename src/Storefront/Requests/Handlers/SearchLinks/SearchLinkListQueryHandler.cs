using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Storefront.Requests.Queries.SearchLinks;
using Vostok.Hotline.Storefront.Services.SearchLinks;
using Vostok.Hotline.Storefront.ViewModels.SearchLinks;

namespace Vostok.Hotline.Storefront.Requests.Handlers.SearchLinks
{
	public class SearchLinkListQueryHandler : IRequestHandler<SearchLinkListQuery, SearchRowsResponseRowModel<SearchLinkViewModel>>
	{
		private readonly SearchLinkService _searchLinkService;

		public SearchLinkListQueryHandler(SearchLinkService searchLinkService)
		{
			_searchLinkService = searchLinkService;
		}

		public async Task<SearchRowsResponseRowModel<SearchLinkViewModel>> Handle(SearchLinkListQuery request, CancellationToken cancellationToken)
		{
			return await _searchLinkService.GetListAsync(request.Count.Value, cancellationToken);
		}
	}
}
