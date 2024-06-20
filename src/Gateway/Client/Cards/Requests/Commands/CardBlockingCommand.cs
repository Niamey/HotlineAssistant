using System;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Enums.Cards;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Commands
{
	public class CardBlockingCommand : IRequest<CardBlockingResultViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? CardId { get; set; }

		[BindRequired]
		public CardBlockingReasonTypeEnum ReasonType { get; set; }

		[BindRequired]
		public string ReasonComment { get; set; }

		[BindRequired]
		public dynamic ReasonInformation { get; set; }
	}
}
