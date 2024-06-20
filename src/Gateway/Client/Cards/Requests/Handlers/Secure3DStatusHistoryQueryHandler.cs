using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class Secure3DStatusHistoryQueryHandler : IRequestHandler<Secure3DStatusHistoryQuery, Secure3DStatusHistoryViewModel>
	{
		private readonly Secure3DService _secure3DService;

		public Secure3DStatusHistoryQueryHandler(Secure3DService secure3DService)
		{
			_secure3DService = secure3DService;
		}

		public async Task<Secure3DStatusHistoryViewModel> Handle(Secure3DStatusHistoryQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _secure3DService.GetStatusHistoryAsync(searchRequest, cancellationToken);
		}
	}
}