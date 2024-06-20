using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Commands
{
	public class TravelDeleteCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }
		[BindRequired]
		public int? TravelId { get; set; }
	}
}
