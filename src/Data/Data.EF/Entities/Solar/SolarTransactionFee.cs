using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarTransactionFee : EntityBase
	{
		public SolarTransactionFee() {
			StmtEntries = new HashSet<SolarStmtEntry>();
		}

		public long? TxnId { get; set; }

		public virtual SolarTransaction Txn { get; set; }

		public long? PrevFeeTxnId { get; set; }
        public long? TxnTypeId { get; set; }
		
		public virtual SolarTransactionType TxnType { get; set; }

		public string TxnDirection { get; set; }
        public string FeeDetails { get; set; }
        public long? AgreementId { get; set; }

		public virtual SolarAgreement Agreement { get; set; }

		public long? AccessorId { get; set; }
        public string AccessorNumber { get; set; }
        public long? FeeRuleId { get; set; }
        public long? NonTxnFeeRuleId { get; set; }
        public long? TrfTariffValueId { get; set; }
        public string Attributes { get; set; }
        public decimal? FeeAmount { get; set; }
        public string FeeCurrency { get; set; }
        public decimal? BillingAmount { get; set; }
        public string BillingCurrency { get; set; }
        public DateTime BankingDate { get; set; }
        public DateTime SystemDate { get; set; }
        public string Status { get; set; }
        public string NextStatus { get; set; }
        public string ExportStatus { get; set; }
        public long Id { get; set; }
        public long? RootFeeId { get; set; }
        public string ResponseCode { get; set; }
        public string AccessorNumberHash { get; set; }
        public long? NonTxnFeeRuleActionId { get; set; }

		public virtual ICollection<SolarStmtEntry> StmtEntries { get; set; }
	}
}
