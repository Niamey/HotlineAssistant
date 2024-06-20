using System;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries
{
	public class TransactionListQuery : IRequest<SearchPagedResponseRowModel<TransactionViewModel>>
	{
		[BindRequired]
		public long? SolarId { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public long? AmountFrom { get; set; }
		public long? AmountTo { get; set; }

		public string CardNumber { get; set; }

		[BindRequired]
		public int? PageIndex { get; set; }

		[BindRequired]
		public int? PageSize { get; set; }
	}
}
