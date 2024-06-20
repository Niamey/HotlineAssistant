using System;
using System.Web;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;
using Vostok.Hotline.Core.Common.Helpers;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Request
{
	public class TransactionSearchRequest : SearchPagedRequestBaseModel
	{
		[BindRequired]
		public long? ClientId { get; set; }

		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public long? AmountFrom { get; set; }
		public long? AmountTo{ get; set; }
		public string CardNumber { get; set; }
		
		public long? TxnId { get; set; }
	}
}