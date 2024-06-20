using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Common.Enums.Transactions;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarTransactionReversalQuery : EntityBase
	{
		public SolarTransactionReversalQuery()
		{			
		}

		public long? TxnId { get; set; }		
	}
}
