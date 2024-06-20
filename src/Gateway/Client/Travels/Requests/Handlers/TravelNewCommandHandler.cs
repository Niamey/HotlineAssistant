using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Services;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers
{
	public class TravelNewCommandHandler : IRequestHandler<TravelNewCommand, StatusCommandViewModel>
	{
		private readonly TravelService _travelService;
		public TravelNewCommandHandler(TravelService travelService)
		{
			_travelService = travelService;
		}
		public async Task<StatusCommandViewModel> Handle(TravelNewCommand request, CancellationToken cancellationToken)
		{
			return await _travelService.NewTravelAsync(request, cancellationToken);
		}
	}
}
