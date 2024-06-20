using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Cards.Services;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Handlers
{
	public class Secure3DInfoQueryHandler : IRequestHandler<Secure3DInfoQuery, Secure3DInfoViewModel>
	{
		private readonly Secure3DService _secure3DService;

		public Secure3DInfoQueryHandler(Secure3DService secure3DService)
		{
			_secure3DService = secure3DService;
		}

		public async Task<Secure3DInfoViewModel> Handle(Secure3DInfoQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _secure3DService.GetStatusAsync(searchRequest, cancellationToken);
		}
	}
}