using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Vicidial.Requests.Commands;
using Vostok.Hotline.Gateway.Vicidial.Services;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Requests.Handlers
{
	public class AreonNewDetailCallCommandHandler : IRequestHandler<AreonNewDetailCallCommand, AreonNewDetailCallResponseViewModel>
	{
		private readonly AreonService _areonService;

		public AreonNewDetailCallCommandHandler(AreonService areonService)
		{
			_areonService = areonService;
		}

		public async Task<AreonNewDetailCallResponseViewModel> Handle(AreonNewDetailCallCommand request, CancellationToken cancellationToken)
		{
			return await _areonService.OpenNewDetailCallAsync(request, cancellationToken);
		}
	}
}
