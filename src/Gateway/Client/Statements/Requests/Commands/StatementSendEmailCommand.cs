using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Statements.Requests.Commands
{
	public class StatementSendEmailCommand : IRequest<StatusCommandViewModel>
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
