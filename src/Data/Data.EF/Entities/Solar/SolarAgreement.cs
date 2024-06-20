using System;
using System.Collections.Generic;
using Vostok.Hotline.Core.Data.Entities;

namespace Vostok.Hotline.Data.EF.Entities.Solar
{
	public class SolarAgreement: EntityBase
	{
		public SolarAgreement()
		{
			OrigTransactions = new HashSet<SolarTransaction>();
			RcvrTransactions = new HashSet<SolarTransaction>();
			Fees = new HashSet<SolarTransactionFee>();
			StmtEntryAgreements = new HashSet<SolarStmtEntryAgreement>();
			Accessors = new HashSet<SolarAccessor>();
		}

		public long? FinInstitutionId { get; set; }
        public long? GdsCodingSchemeId { get; set; }
        public long? ProductId { get; set; }
        public long? ClientId { get; set; }
        public string AgrNumber { get; set; }
        public string Description { get; set; }
        public string AgreementClass { get; set; }
        public string ServiceType { get; set; }
        public string Currency { get; set; }
        public DateTime SignatureDate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public DateTime? BankingDate { get; set; }
        public DateTime? LastBillingDate { get; set; }
        public string Attributes { get; set; }
        public long? BranchId { get; set; }
        public long Id { get; set; }
        public DateTime AuditDate { get; set; }
        public string AuditState { get; set; }
        public long AuditUserId { get; set; }
        public string DefaultMcc { get; set; }
        public string ExtraCurrencyList { get; set; }
        public string Isolated { get; set; }
        public string IsOverridingMulticurrency { get; set; }
        public string Name { get; set; }
        public string Configuration { get; set; }
		
		public virtual ICollection<SolarTransaction> OrigTransactions { get; set; }
		public virtual ICollection<SolarTransaction> RcvrTransactions { get; set; }
		public virtual ICollection<SolarTransactionFee> Fees { get; set; }
		public virtual ICollection<SolarStmtEntryAgreement> StmtEntryAgreements { get; set; }
		public virtual ICollection<SolarAccessor> Accessors { get; set; }
	}
}
