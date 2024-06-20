using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Enums.Transactions;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarAccessor : EntityBase
	{
		public long AccessorId { get; set; }
		public long AccessorTypeId { get; set; }
		public long? AgreementId { get; set; }
		
		public virtual SolarAgreement Agreement { get; set; }

		public string AccessNumber { get; set; }
		public int IsDelete { get; set; }
		public long? ClientId { get; set; }
		public long? ParentAccessorId { get; set; }
	}
}
