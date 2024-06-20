using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarTransactionType : EntityBase
	{
		public SolarTransactionType()
		{
			Transactions = new HashSet<SolarTransaction>();
			Fees = new HashSet<SolarTransactionFee>();
			StmtEntries = new HashSet<SolarStmtEntry>();
		}

		public string ChargebackStage { get; set; }
        public string Direction { get; set; }
        public string OperationClass { get; set; }
        public string IsStandard { get; set; }
        public long Id { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditState { get; set; }
        public long AuditUserId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AgreementClass { get; set; }
        public string MayBePartial { get; set; }

		public virtual ICollection<SolarTransaction> Transactions { get; set; }
		public virtual ICollection<SolarTransactionFee> Fees { get; set; }
		public virtual ICollection<SolarStmtEntry> StmtEntries { get; set; }
		
	}
}
