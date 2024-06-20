using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Travels.Services;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers
{
	public class TravelDeleteCommandHandler : IRequestHandler<TravelDeleteCommand, StatusCommandViewModel>
	{
		private readonly TravelService _travelService;
		public TravelDeleteCommandHandler(TravelService travelService)
		{
			_travelService = travelService;
		}
		public async Task<StatusCommandViewModel> Handle(TravelDeleteCommand request, CancellationToken cancellationToken)
		{
			return await _travelService.DeleteTravelAsync(request, cancellationToken);
		}
	}
}
