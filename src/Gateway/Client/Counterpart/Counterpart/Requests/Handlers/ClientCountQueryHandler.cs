using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Counterpart.Services;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Handlers
{
	public class ClientCountQueryHandler : IRequestHandler<ClientCountQuery, ClientCountViewModel>
	{
		private readonly ClientService _clientService;

		public ClientCountQueryHandler(ClientService clientService)
		{
			_clientService = clientService;
		}

		public async Task<ClientCountViewModel> Handle(ClientCountQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartCountSearchRequest
			{
				 SearchFilter = request.SearchFilter,
				 SearchType = request.SearchType
			};

			return await _clientService.ClientCountAsync(searchRequest, cancellationToken);			
		}
	}
}
