using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Api.CRM.SearchRequests;
using Vostok.Hotline.Gateway.Client.Counterpart.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Counterpart.Services;
using Vostok.Hotline.Gateway.Client.Counterpart.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Requests.Handlers
{
	public class ClientDetailQueryHandler : IRequestHandler<ClientDetailQuery, ClientViewModel>
	{
		private readonly ClientService _clientService;

		public ClientDetailQueryHandler(ClientService clientService)
		{
			_clientService = clientService;
		}

		public async Task<ClientViewModel> Handle(ClientDetailQuery request, CancellationToken cancellationToken)
		{
			var searchRequest = new CounterpartSearchRequest
			{
				SearchFilter = request.SolarId.ToString(),
				SearchType = Api.CRM.Enums.SearchTypeEnum.SolarId
			};

			return await _clientService.ClientDetailAsync(searchRequest, cancellationToken);
		}
	}
}
