using Microsoft.EntityFrameworkCore;
using Vostok.Hotline.Data.EF.Entities.Solar;

namespace Vostok.Hotline.Data.EF.Configurations.Solar.Entities
{
	public static class SolarStmtEntryEntityConfiguration
	{
		public static void ConfigureHotlineSolarStmtEntryEntity(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<SolarStmtEntry> (entity =>
			{
				entity.HasKey(x => x.Id);
				entity.ToView("vw_bo_stmt_entry", "SOLAR_BO");

				entity.Property(e => e.Id)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ID");

				entity.Property(e => e.AcqAccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ACQ_ACCESSOR_ID");

				entity.Property(e => e.AcqAgmtId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ACQ_AGMT_ID");

				entity.Property(e => e.AcqAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ACQ_AMOUNT");

				entity.Property(e => e.AcqClientId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ACQ_CLIENT_ID");

				entity.Property(e => e.AcqCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("ACQ_CURRENCY");

				entity.Property(e => e.AcqFeesAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ACQ_FEES_AMOUNT");

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

				entity.Property(e => e.BankingDate)
					.HasColumnName("BANKING_DATE");

				entity.Property(e => e.CardAcceptorId)
					.HasMaxLength(64)
					.IsUnicode(false)
					.HasColumnName("CARD_ACCEPTOR_ID");

				entity.Property(e => e.CreditPartnerFeeAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("CREDIT_PARTNER_FEE_AMOUNT");

				entity.Property(e => e.DebitPartnerFeeAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("DEBIT_PARTNER_FEE_AMOUNT");

				entity.Property(e => e.Description)
					.HasMaxLength(4000)
					.IsUnicode(false)
					.HasColumnName("DESCRIPTION");

				entity.Property(e => e.EntryType)
					.IsRequired()
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("ENTRY_TYPE");

				entity.Property(e => e.FeeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("FEE_ID");

				entity.HasOne(d => d.Fee)
					.WithMany(p => p.StmtEntries)
					.HasForeignKey(m => m.FeeId);

				entity.Property(e => e.IssAccessorId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ISS_ACCESSOR_ID");

				entity.Property(e => e.IssAccessorNumber)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ISS_ACCESSOR_NUMBER");

				entity.Property(e => e.IssAgmtId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ISS_AGMT_ID");

				entity.Property(e => e.IssAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_AMOUNT");

				entity.Property(e => e.IssAmountAvailable)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_AMOUNT_AVAILABLE");

				entity.Property(e => e.IssClientId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ISS_CLIENT_ID");

				entity.Property(e => e.IssCurrency)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("ISS_CURRENCY");

				entity.Property(e => e.IssFeesAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_FEES_AMOUNT");

				entity.Property(e => e.IssInstallmentAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_INSTALLMENT_AMOUNT");

				entity.Property(e => e.IssInstallmentDuration)
					.HasColumnType("numeric(10, 0)")
					.HasColumnName("ISS_INSTALLMENT_DURATION");

				entity.Property(e => e.IssInstallmentFeeAmount)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_INSTALLMENT_FEE_AMOUNT");

				entity.Property(e => e.IssUnusedCreditLimit)
					.HasColumnType("numeric(30, 10)")
					.HasColumnName("ISS_UNUSED_CREDIT_LIMIT");

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

				entity.Property(e => e.Orn)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("ORN");

				entity.Property(e => e.PrevFeeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("PREV_FEE_ID");

				entity.Property(e => e.PrevTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("PREV_TXN_ID");

				entity.Property(e => e.ResponseCode)
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("RESPONSE_CODE");

				entity.Property(e => e.RootTxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("ROOT_TXN_ID");

				entity.Property(e => e.Rrn)
					.HasMaxLength(32)
					.IsUnicode(false)
					.HasColumnName("RRN");

				entity.Property(e => e.Status)
					.HasMaxLength(2)
					.IsUnicode(false)
					.HasColumnName("STATUS");

				entity.Property(e => e.StmtDate)
					.HasColumnName("STMT_DATE");

				entity.Property(e => e.TerminalId)
					.HasMaxLength(256)
					.IsUnicode(false)
					.HasColumnName("TERMINAL_ID");

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
					.HasMaxLength(3)
					.IsUnicode(false)
					.HasColumnName("TXN_CURRENCY");

				entity.Property(e => e.TxnDate)
					.HasColumnName("TXN_DATE");

				entity.Property(e => e.TxnDirection)
					.HasMaxLength(1)
					.IsUnicode(false)
					.HasColumnName("TXN_DIRECTION");

				entity.Property(e => e.TxnId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TXN_ID");

				entity.HasOne(d => d.Txn)
					.WithMany(p => p.StmtEntries)
					.HasForeignKey(m => m.TxnId);

				entity.Property(e => e.TxnTypeId)
					.HasColumnType("numeric(18, 0)")
					.HasColumnName("TXN_TYPE_ID");

				entity.HasOne(d => d.TxnType)
					.WithMany(p => p.StmtEntries)
					.HasForeignKey(m => m.TxnTypeId);
			});
		}
	}
}
