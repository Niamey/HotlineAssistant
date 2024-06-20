using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Commands
{
	public class CardTariffSendEmailCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? ClientId { get; set; }
		[BindRequired]
		public long? CardId { get; set; }
	}
}
