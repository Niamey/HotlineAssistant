﻿using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class CardDebtToBankQuery : IRequest<CardDebtToBankViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? CardId { get; set; }
	}
}
