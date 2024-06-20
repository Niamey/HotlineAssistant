using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Enums.MDES;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Commands
{
	public class CardTokenDeleteCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? CardId { get; set; }

		[BindRequired]
		public string TokenUniqueReference { get; set; }

		public string CommentText { get; set; }

		[BindRequired]
		public ReasonTypeMdesEnum? ReasonCode { get; set; }

		public bool? BlockMobApp { get; set; }
	}
}
