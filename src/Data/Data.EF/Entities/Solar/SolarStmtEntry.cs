using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
    public class SolarStmtEntry : EntityBase
	{
		public SolarStmtEntry() {
			StmtEntryAgreements = new HashSet<SolarStmtEntryAgreement>();
		}

		public string EntryType { get; set; }
        public long? TxnId { get; set; }

		public virtual SolarTransaction Txn { get; set; }

		public long? RootTxnId { get; set; }
        public long? PrevTxnId { get; set; }
        public long? AuthTxnId { get; set; }
        public long? FeeId { get; set; }

		public virtual SolarTransactionFee Fee { get; set; }

		public long? PrevFeeId { get; set; }
        public string Status { get; set; }
        public long? TxnTypeId { get; set; }

		public virtual SolarTransactionType TxnType { get; set; }

		public string TxnCategory { get; set; }
        public string TxnClass { get; set; }
        public string TxnDirection { get; set; }
        public DateTime? TxnDate { get; set; }
        public DateTime? BankingDate { get; set; }
        public DateTime? StmtDate { get; set; }
        public string Orn { get; set; }
        public string AuthCode { get; set; }
        public string Rrn { get; set; }
        public string Description { get; set; }
        public string TerminalId { get; set; }
        public string CardAcceptorId { get; set; }
        public string Mcc { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCountry { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantState { get; set; }
        public decimal? TxnAmount { get; set; }
        public string TxnCurrency { get; set; }
        public decimal? AcqAmount { get; set; }
        public decimal? AcqFeesAmount { get; set; }
        public string AcqCurrency { get; set; }
        public long? AcqClientId { get; set; }
        public long? AcqAgmtId { get; set; }
        public long? AcqAccessorId { get; set; }
        public decimal? IssAmount { get; set; }
        public decimal? IssFeesAmount { get; set; }
        public string IssCurrency { get; set; }
        public decimal? IssAmountAvailable { get; set; }
        public decimal? IssUnusedCreditLimit { get; set; }
        public string IssAccessorNumber { get; set; }
        public long? IssClientId { get; set; }
        public long? IssAgmtId { get; set; }
        public long? IssAccessorId { get; set; }
        public long? IssInstallmentDuration { get; set; }
        public decimal? IssInstallmentAmount { get; set; }
        public decimal? IssInstallmentFeeAmount { get; set; }
        public decimal? DebitPartnerFeeAmount { get; set; }
        public decimal? CreditPartnerFeeAmount { get; set; }
        public string ResponseCode { get; set; }
        public string Attributes { get; set; }
        public long Id { get; set; }

		public virtual ICollection<SolarStmtEntryAgreement> StmtEntryAgreements { get; set; }
	}
}
