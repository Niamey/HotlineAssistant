using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Commands
{
	public class CardUnlockingLockCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? CardId { get; set; }
		public string Comment { get; set; }
	}
}
