using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Queries
{
	public class TravelDetailQuery : IRequest<TravelViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }
		[BindRequired]
		public int? TravelId { get; set; }

	}
}
