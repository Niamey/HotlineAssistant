using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Travels.Services;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers
{
	public class TravelDetailQueryHandler : IRequestHandler<TravelDetailQuery, TravelViewModel>
	{
		private readonly TravelService _travelService;
		public TravelDetailQueryHandler(TravelService travelService)
		{
			_travelService = travelService;
		}

		public async Task<TravelViewModel> Handle(TravelDetailQuery request, CancellationToken cancellationToken)
		{
			return await _travelService.GetTravelAsync(request, cancellationToken);
		}
	}
}
