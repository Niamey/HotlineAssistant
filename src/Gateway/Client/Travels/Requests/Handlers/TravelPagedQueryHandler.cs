using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Travels.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Travels.Services;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Handlers
{
	public class TravelPagedQueryHandler : IRequestHandler<TravelPagedQuery, SearchPagedResponseRowModel<TravelViewModel>>
	{
		private readonly TravelService _travelService;
		public TravelPagedQueryHandler(TravelService travelService)
		{
			_travelService = travelService;
		}
		  
		public async Task<SearchPagedResponseRowModel<TravelViewModel>> Handle(TravelPagedQuery request, CancellationToken cancellationToken)
		{
			return await _travelService.GetTravelPagedAsync(request, cancellationToken);
		}
	}
}
