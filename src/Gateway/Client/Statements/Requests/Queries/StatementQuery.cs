using System;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Statements.Requests.Queries
{
	public class StatementQuery : IRequest<StatementViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public long? AgreementId { get; set; }

		[BindRequired]
		public DateTime? DateStart { get; set; }

		[BindRequired]
		public DateTime? DateEnd { get; set; }
	}
}
