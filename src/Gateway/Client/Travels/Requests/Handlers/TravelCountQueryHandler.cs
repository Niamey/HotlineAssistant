using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Travels.Services;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers
{
	public class TravelCountQueryHandler : IRequestHandler<TravelCountQuery, TravelCountViewModel>
	{
		private readonly TravelService _travelService;

		public TravelCountQueryHandler(TravelService travelService)
		{
			_travelService = travelService;
		}

		public async Task<TravelCountViewModel> Handle(TravelCountQuery request, CancellationToken cancellationToken)
		{
			return await _travelService.TravelCountAsync(request, cancellationToken);
		}
	}
}
