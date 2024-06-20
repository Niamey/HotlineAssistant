using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarTransaction : EntityBase
	{
		public SolarTransaction()
		{
			Fees = new HashSet<SolarTransactionFee>();
			StmtEntries = new HashSet<SolarStmtEntry>();
			// AuthTransactions = new HashSet<SolarTransaction>();

			//ParentTransactions = new HashSet<SolarTransactionQuery>();
			//ChildTransactions = new HashSet<SolarTransactionQuery>();
		}

		public long? RootTxnId { get; set; }
        public long? PrevTxnId { get; set; }
        public long? AuthTxnId { get; set; }
		// public virtual SolarTransaction AuthTxn { get; set; }

		public string RtTxnCode { get; set; }
        public long? TxnTypeId { get; set; }

		public virtual SolarTransactionType TxnType { get; set; }
		
		public string TxnClass { get; set; }
        public string TxnCategory { get; set; }
        public string TxnDirection { get; set; }
        public string TxnDetails { get; set; }
        public string AuthCode { get; set; }
        public string IssRefNumber { get; set; }
        public string AcqRefNumber { get; set; }
        public string RetRefNumber { get; set; }
        public string OrigRefNumber { get; set; }
        public long? OrigRtSystemId { get; set; }
        public string OrigMemberId { get; set; }
        public string OrigNumber { get; set; }
        public long? OrigAgreementId { get; set; }

		public virtual SolarAgreement OrigAgreement { get; set; }

		public long? OrigAccessorId { get; set; }
        public long? RcvrRtSystemId { get; set; }
        public string RcvrMemberId { get; set; }
        public string RcvrNumber { get; set; }
        public long? RcvrAgreementId { get; set; }

		public virtual SolarAgreement RcvrAgreement { get; set; }

		public long? RcvrAccessorId { get; set; }
        public long? OrigTxnRuleId { get; set; }
        public long? OrigTxnRuleCondId { get; set; }
        public long? OrigTxnRuleActId { get; set; }
        public long? RcvrTxnRuleId { get; set; }
        public long? RcvrTxnRuleCondId { get; set; }
        public long? RcvrTxnRuleActId { get; set; }
        public long? OrigBalItemTypeId { get; set; }
        public long? RcvrBalItemTypeId { get; set; }
        public string CardAcceptorId { get; set; }
        public string Mcc { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCountry { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantState { get; set; }
        public string Attributes { get; set; }
        public decimal TxnAmount { get; set; }
        public string TxnCurrency { get; set; }
        public decimal SettlAmount { get; set; }
        public string SettlCurrency { get; set; }
        public decimal? OrigAmount { get; set; }
        public string OrigCurrency { get; set; }
        public decimal? OrigBillingAmount { get; set; }
        public string OrigBillingCurrency { get; set; }
        public decimal? RcvrAmount { get; set; }
        public string RcvrCurrency { get; set; }
        public decimal? RcvrBillingAmount { get; set; }
        public string RcvrBillingCurrency { get; set; }
        public DateTime TxnDate { get; set; }
        public DateTime? BankingDate { get; set; }
        public DateTime SystemDate { get; set; }
        public string ResponseCode { get; set; }
        public string Status { get; set; }
        public string NextStatus { get; set; }
        public string MatchStatus { get; set; }
        public string ExportStatus { get; set; }
        public long Id { get; set; }
        public string OrigNumberHash { get; set; }
        public string RcvrNumberHash { get; set; }
        public string TraceRefNumber { get; set; }

		public virtual ICollection<SolarTransactionFee> Fees { get; set; }
		public virtual ICollection<SolarStmtEntry> StmtEntries { get; set; }
		// public virtual ICollection<SolarTransaction> AuthTransactions { get; set; }

		//public virtual ICollection<SolarTransactionQuery> ParentTransactions { get; set; }
		//public virtual ICollection<SolarTransactionQuery> ChildTransactions { get; set; }
	}
}
