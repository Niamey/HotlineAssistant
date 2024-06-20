using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.Services;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Requests.Handlers
{
	public class AreonNewCallCommandHandler : IRequestHandler<AreonNewCallCommand, AreonNewCallResponseViewModel>
	{
		private readonly AreonService _areonService;

		public AreonNewCallCommandHandler(AreonService areonService)
		{
			_areonService = areonService;
		}

		public async Task<AreonNewCallResponseViewModel> Handle(AreonNewCallCommand request, CancellationToken cancellationToken)
		{
			return await _areonService.OpenNewCallAsync(request, cancellationToken);
		}
	}
}
