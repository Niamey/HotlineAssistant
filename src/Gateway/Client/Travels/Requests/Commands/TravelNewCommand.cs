using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;

namespace Vostok.Hotline.Gateway.Client.Travels.Requests.Commands
{
	public class TravelNewCommand : IRequest<StatusCommandViewModel>
	{
		[BindRequired]
		public long? SolarId { get; set; }
		[BindRequired]
		public int? TravelId { get; set; }
		[BindRequired]
		public List<TravelCountryCommand> Countries { get; set; }
		[BindRequired]
		public DateTime? StartTravel { get; set; }
		[BindRequired]
		public DateTime? EndTravel { get; set; }
		[BindRequired]
		public long? ContactPhone { get; set; }
		[BindRequired]
		public List<TravelCardCommand> Cards { get; set; }
		[BindRequired]
		public decimal? CashWithdrawalLimit { get; set; }
		[BindRequired]
		public decimal? LimitCardTransfers { get; set; }

		[BindRequired]
		public bool? IsClientAbroad { get; set; }

	}
}
