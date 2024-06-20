using System;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Cards.Requests.Commands
{
	public class CardLimitRiskChangeCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? ClientId { get; set; }
		[BindRequired]
		public long? CardId { get; set; }
		[BindRequired]
		public decimal? Limit { get; set; }
		[BindRequired]
		public bool? IsP2PLimited { get; set; }
		public DateTime? LimitSetDate { get; set; }
		public string Phone { get; set; }
	}
}
