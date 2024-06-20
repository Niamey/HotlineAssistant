using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Core.Common.Base.Abstractions.Requests;

namespace Vostok.Hotline.Data.Repository.Solar.Models.Request
{
	public class StatementSearchRequest
	{
		public long ClientId { get; set; }
		public long AgreementId { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
	}
}
