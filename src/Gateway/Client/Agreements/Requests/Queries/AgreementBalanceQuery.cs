﻿using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Gateway.Client.Agreements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Agreements.Requests.Queries
{
	public class AgreementBalanceQuery : IRequest<AgreementBalanceViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? AgreementId { get; set; }
	}
}
