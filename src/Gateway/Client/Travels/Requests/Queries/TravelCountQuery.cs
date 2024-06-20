using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Travels.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Queries
{
	public class TravelCountQuery : IRequest<TravelCountViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }
	}
}
