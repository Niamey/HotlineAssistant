using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarTransactionEntityConfiguration
	{
		public static void ConfigureHotlineSolarTransactionEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarTransaction>(entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToView("vw_bo_txn", "SOLAR_BO");

				entity.Property(e => e.Id)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ID");

				entity.Property(e => e.AcqRefNumber)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("ACQ_REF_NUMBER");

				entity.Property(e => e.Attributes)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("ATTRIBUTES");

				entity.Property(e => e.AuthCode)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("AUTH_CODE");

				entity.Property(e => e.AuthTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("AUTH_TXN_ID");
				/*
				entity.HasOne(d => d.AuthTxn)
					.WithMany(p => p.AuthTransactions)
					.HasForeignKey(m => m.AuthTxnId);
				*/
				entity.Property(e => e.BankingDate)
					.HasColumnName("BANKING_DATE");

				entity.Property(e => e.CardAcceptorId)
					.HasMaxLength(64)
					.IsUnicode(false)
					.HasColumnName("CARD_ACCEPTOR_ID");

				entity.Property(e => e.ExportStatus)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("EXPORT_STATUS");

				entity.Property(e => e.IssRefNumber)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("ISS_REF_NUMBER");

				entity.Property(e => e.MatchStatus)
					.IsRequired()
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("MATCH_STATUS");

				entity.Property(e => e.Mcc)
					.HasMaxLength(4)
					.IsUnicode(false)
					.HasColumnName("MCC");

				entity.Property(e => e.MerchantCity)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("MERCHANT_CITY");

				entity.Property(e => e.MerchantCountry)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("MERCHANT_COUNTRY");

				entity.Property(e => e.MerchantName)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("MERCHANT_NAME");

				entity.Property(e => e.MerchantState)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("MERCHANT_STATE");

				entity.Property(e => e.NextStatus)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("NEXT_STATUS");

				entity.Property(e => e.OrigAccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_ACCESSOR_ID");

				entity.Property(e => e.OrigAgreementId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_AGREEMENT_ID");

				entity.HasOne(d => d.OrigAgreement)
					.WithMany(p => p.OrigTransactions)
					.HasForeignKey(m => m.OrigAgreementId);

				/*
				 modelBuilder.Entity<Course>()
    .HasMany(t => t.Instructors)
    .WithMany(t => t.Courses)
    .Map(m =>
    {
        m.ToTable("CourseInstructor");
        m.MapLeftKey("CourseID");
        m.MapRightKey("InstructorID");
    });
				 */

				entity.Property(e => e.OrigAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ORIG_AMOUNT");

				entity.Property(e => e.OrigBalItemTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_BAL_ITEM_TYPE_ID");

				entity.Property(e => e.OrigBillingAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ORIG_BILLING_AMOUNT");

				entity.Property(e => e.OrigBillingCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("ORIG_BILLING_CURRENCY");

				entity.Property(e => e.OrigCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("ORIG_CURRENCY");

				entity.Property(e => e.OrigMemberId)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("ORIG_MEMBER_ID");

				entity.Property(e => e.OrigNumber)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ORIG_NUMBER");

				entity.Property(e => e.OrigNumberHash)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ORIG_NUMBER_HASH");

				entity.Property(e => e.OrigRefNumber)
					.HasMaxLength(64)
					.IsUnicode(false)
					.HasColumnName("ORIG_REF_NUMBER");

				entity.Property(e => e.OrigRtSystemId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_RT_SYSTEM_ID");

				entity.Property(e => e.OrigTxnRuleActId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_TXN_RULE_ACT_ID");

				entity.Property(e => e.OrigTxnRuleCondId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_TXN_RULE_COND_ID");

				entity.Property(e => e.OrigTxnRuleId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ORIG_TXN_RULE_ID");

				entity.Property(e => e.PrevTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("PREV_TXN_ID");

				entity.Property(e => e.RcvrAccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_ACCESSOR_ID");

				entity.Property(e => e.RcvrAgreementId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_AGREEMENT_ID");

				entity.HasOne(d => d.RcvrAgreement)
					.WithMany(p => p.RcvrTransactions)
					.HasForeignKey(m => m.RcvrAgreementId);

				entity.Property(e => e.RcvrAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("RCVR_AMOUNT");

				entity.Property(e => e.RcvrBalItemTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_BAL_ITEM_TYPE_ID");

				entity.Property(e => e.RcvrBillingAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("RCVR_BILLING_AMOUNT");

				entity.Property(e => e.RcvrBillingCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("RCVR_BILLING_CURRENCY");

				entity.Property(e => e.RcvrCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("RCVR_CURRENCY");

				entity.Property(e => e.RcvrMemberId)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("RCVR_MEMBER_ID");

				entity.Property(e => e.RcvrNumber)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("RCVR_NUMBER");

				entity.Property(e => e.RcvrNumberHash)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("RCVR_NUMBER_HASH");

				entity.Property(e => e.RcvrRtSystemId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_RT_SYSTEM_ID");

				entity.Property(e => e.RcvrTxnRuleActId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_TXN_RULE_ACT_ID");

				entity.Property(e => e.RcvrTxnRuleCondId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_TXN_RULE_COND_ID");

				entity.Property(e => e.RcvrTxnRuleId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("RCVR_TXN_RULE_ID");

				entity.Property(e => e.ResponseCode)
					.IsRequired()
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("RESPONSE_CODE");

				entity.Property(e => e.RetRefNumber)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("RET_REF_NUMBER");

				entity.Property(e => e.RootTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ROOT_TXN_ID");

				entity.Property(e => e.RtTxnCode)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("RT_TXN_CODE");

				entity.Property(e => e.SettlAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("SETTL_AMOUNT");

				entity.Property(e => e.SettlCurrency)
					.IsRequired()
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("SETTL_CURRENCY");

				entity.Property(e => e.Status)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("STATUS");

				entity.Property(e => e.SystemDate)
					.HasColumnName("SYSTEM_DATE");

				entity.Property(e => e.TraceRefNumber)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("TRACE_REF_NUMBER");

				entity.Property(e => e.TxnAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("TXN_AMOUNT");

				entity.Property(e => e.TxnCategory)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("TXN_CATEGORY");

				entity.Property(e => e.TxnClass)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("TXN_CLASS");

				entity.Property(e => e.TxnCurrency)
					.IsRequired()
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("TXN_CURRENCY");

				entity.Property(e => e.TxnDate)
					.HasColumnName("TXN_DATE");

				entity.Property(e => e.TxnDetails)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("TXN_DETAILS");

				entity.Property(e => e.TxnDirection)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("TXN_DIRECTION");

				entity.Property(e => e.TxnTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TXN_TYPE_ID");

				entity.HasOne(d => d.TxnType)
					.WithMany(p => p.Transactions)
					.HasForeignKey(m => m.TxnTypeId);
			});
		}
	}
}
