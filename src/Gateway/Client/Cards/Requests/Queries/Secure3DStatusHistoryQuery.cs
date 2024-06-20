using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class Secure3DStatusHistoryQuery : IRequest<Secure3DStatusHistoryViewModel>
	{
		[Required]
		public long? SolarId { get; set; }

		[Required]
		public long? CardId { get; set; }
	}
}