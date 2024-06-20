using System.ComponentModel.DataAnnotations;
using MediatR;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class SmsInfoHistoryStatusQuery : IRequest<SmsInfoHistoryStatusViewModel>
	{
		[Required]
		public long? SolarId { get; set; }
		[Required]
		public long? CardId { get; set; }
	}
}
