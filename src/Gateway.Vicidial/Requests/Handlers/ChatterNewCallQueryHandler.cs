using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Vicidial.Requests.Queries;
using Vostok.Hotline.Gateway.Vicidial.Services;
using Vostok.Hotline.Gateway.Vicidial.ViewModels;

namespace Vostok.Hotline.Gateway.Vicidial.Requests.Handlers
{
	public class ChatterNewCallQueryHandler : IRequestHandler<ChatterNewCallQuery, ChatterNewCallResponseViewModel>
	{
		private readonly ChatterService _chatterService;

		public ChatterNewCallQueryHandler(ChatterService chatterService)
		{
			_chatterService = chatterService;
		}

		public async Task<ChatterNewCallResponseViewModel> Handle(ChatterNewCallQuery request, CancellationToken cancellationToken)
		{
			return await _chatterService.OpenNewCallAsync(request, cancellationToken);
		}
	}
}
