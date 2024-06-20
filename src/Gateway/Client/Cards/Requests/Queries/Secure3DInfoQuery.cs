﻿using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Cards.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Queries
{
	public class Secure3DInfoQuery : IRequest<Secure3DInfoViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? CardId { get; set; }
	}
}