using System;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries
{
	public class TransactionListFInancialBlockingQuery : IRequest<SearchPagedResponseRowModel<TransactionFinancialViewModel>>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		[BindRequired]
		public DateTime? DateFrom { get; set; }

		[BindRequired]
		public DateTime? DateTo { get; set; }

		[BindRequired]
		public int? PageIndex { get; set; }

		[BindRequired]
		public int? PageSize { get; set; }
	}
}
