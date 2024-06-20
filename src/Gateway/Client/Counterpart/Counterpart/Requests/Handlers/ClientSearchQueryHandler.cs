using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Enums;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Counterpart.Services;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Handlers
{
	public class ClientSearchQueryHandler : IRequestHandler<ClientSearchQuery, SearchPagedResponseRowModel<ClientViewModel>>
	{
		private readonly ClientService _clientService;

		public ClientSearchQueryHandler(ClientService clientService)
		{
			_clientService = clientService;
		}

		public async Task<SearchPagedResponseRowModel<ClientViewModel>> Handle(ClientSearchQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartSearchRequest
			{
				SearchFilter = request.SearchFilter,
				SearchType = request.SearchType,
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				SortColumn = request.SortColumn,
				SortDirection = request.SortDirection
			};

			return await _clientService.ClientListAsync(searchRequest, cancellationToken);
		}
	}
}
