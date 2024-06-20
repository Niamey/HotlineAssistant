using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Application.Parsers;
using Vostok.HotLineAssistant.Common;
using Vostok.HotLineAssistant.Common.Helpers;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.ContactManager.Application.Client;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Queries;
using Vostok.HotLineAssistant.ContactManager.Domain.Clients;

namespace Vostok.HotLineAssistant.ContactManager.Infrastructure.QueryHandlers.Clients
{
	public class ClientSearchQueryHandler : IRequestHandler<ClientSearchQuery, PagedResponseEx<ClientDto>>
	{
		private readonly IClientService _clientService;
		private readonly IMapper _mapper;

		public ClientSearchQueryHandler(IClientService clientService, IMapper mapper)
		{
			_clientService = clientService;
			_mapper = mapper;
		}

		public async Task<PagedResponseEx<ClientDto>> Handle(ClientSearchQuery request, CancellationToken cancellationToken)
		{
			var criteria = new ClientSearchCriteria
			{
				Criteria = request.Criteria,
				Code = ParseEnum.Parse<CriteriaTypes>(request.Code),
				Type = IdentifierTypes.None,
				CurrentPage = request.PageNumber,
				PageSize = request.PageSize,
				SortColumn = request.SortColumn,
				IsDescending = request.SortDirection != null && Convert.ToBoolean(request.SortDirection.Value)
			};

			var pageReq = new ByPageRequest {PageNumber = request.PageNumber, PageSize = request.PageSize};
			var item = await _clientService.ClientSearch(criteria);
			var items = _mapper.Map<List<ClientDto>>(item.Rows);
			var resp = new PagedResponseEx<ClientDto>(PageOptions.From(pageReq), Int32.MaxValue, items.ToArray())
			{
				IsNextPageAvailable = item.IsNextPageAvailable
			};
			return resp;
		}
	}
}